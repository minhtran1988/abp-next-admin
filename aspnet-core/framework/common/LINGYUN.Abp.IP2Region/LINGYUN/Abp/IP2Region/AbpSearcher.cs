// Copyright 2025 The Ip2Region Authors. All rights reserved.
// Use of this source code is governed by a Apache2.0-style
// license that can be found in the LICENSE file.
// @Author Alan <lzh.shap@gmail.com>
// @Date   2023/07/25
// Updated by Argo Zhang <argo@live.ca> at 2025/11/21

using IP2Region.Net.Abstractions;
using IP2Region.Net.Internal;
using IP2Region.Net.XDB;
using System;
using System.Buffers.Binary;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace LINGYUN.Abp.IP2Region;

/// <summary>
/// ip2region offline queryer
/// </summary>
/// <remarks>
/// Supports two offline data files, IPv4 and IPv6:
/// * <see cref="AbpSearcher(CachePolicy, Stream)"/>: Only load one data file, and select the comparison method according to the byte length of the address when querying
/// (IPv4 is 4 bytes, IPv6 is 16 bytes), that is, the caller ensures that the version of the data file is consistent with the query address;
/// * <see cref="AbpSearcher(CachePolicy, Stream, Stream?)"/>: Load IPv4 and IPv6 data files at the same time, automatically route according to address family,
/// Return <c>null</c> when the data file corresponding to the address family is not provided (to avoid using the wrong data file to parse the wrong geographical location).
/// 
/// IPv4-mapped IPv6 addresses (such as <c>::ffff:1.2.3.4</c>) will be normalized to IPv4 queries.
/// </remarks>
public class AbpSearcher : ISearcher
{
    private readonly ICacheStrategy _cacheStrategy;
    private readonly ICacheStrategy? _ipv6CacheStrategy;
    private readonly bool _isDualDatabase;

    /// <summary>
    /// Only load one data file (IPv4 or IPv6)
    /// </summary>
    /// <param name="cachePolicy">Cache Policy</param>
    /// <param name="xdbStream">Data file stream</param>
    public AbpSearcher(CachePolicy cachePolicy, Stream xdbStream)
        : this(cachePolicy, xdbStream, null, false)
    {
    }

    /// <summary>
    /// Load IPv4 and IPv6 data files at the same time, automatically route according to address family
    /// </summary>
    /// <param name="cachePolicy">Cache Policy</param>
    /// <param name="xdbStream">IPv4 data file stream</param>
    /// <param name="ipv6XdbStream">IPv6 data file stream, if not provided, IPv6 query returns <c>null</c></param>
    public AbpSearcher(CachePolicy cachePolicy, Stream xdbStream, Stream? ipv6XdbStream)
        : this(cachePolicy, xdbStream, ipv6XdbStream, true)
    {
    }

    private AbpSearcher(CachePolicy cachePolicy, Stream xdbStream, Stream? ipv6XdbStream, bool isDualDatabase)
    {
        _cacheStrategy = CacheStrategyFactory.CreateCacheStrategy(cachePolicy, xdbStream);

        _isDualDatabase = isDualDatabase;
        if (ipv6XdbStream != null)
        {
            _ipv6CacheStrategy = CacheStrategyFactory.CreateCacheStrategy(cachePolicy, ipv6XdbStream);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public int IoCount => _cacheStrategy.IoCount + (_ipv6CacheStrategy?.IoCount ?? 0);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? Search(string ipStr) => Search(IPAddress.Parse(ipStr));

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? Search(IPAddress ipAddress)
    {
        // IPv4 mapped IPv6 address is normalized to IPv4 query
        if (ipAddress.IsIPv4MappedToIPv6)
        {
            ipAddress = ipAddress.MapToIPv4();
        }

        var cacheStrategy = ResolveCacheStrategy(ipAddress);
        if (cacheStrategy == null)
        {
            return null;
        }

        return SearchCore(cacheStrategy, ipAddress.GetAddressBytes());
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Obsolete("Deprecated; please use Search(string) or Search(IPAddress) method.")]
    [ExcludeFromCodeCoverage]
    public string? Search(uint ipAddress)
    {
        var bytes = BitConverter.GetBytes(ipAddress);
        Array.Reverse(bytes);

        return SearchCore(_cacheStrategy, bytes);
    }

    /// <summary>
    /// Select data files by address family
    /// </summary>
    /// <remarks>
    /// No routing is done in single file mode (only one data file is loaded), and the behavior of comparing by byte length is used.
    /// </remarks>
    private ICacheStrategy? ResolveCacheStrategy(IPAddress ipAddress)
    {
        if (ipAddress.AddressFamily == AddressFamily.InterNetworkV6)
        {
            if (_ipv6CacheStrategy != null)
            {
                return _ipv6CacheStrategy;
            }

            // IPv6 data files are missing in dual-file mode, and IPv4 data file parsing is not used.
            return _isDualDatabase ? null : _cacheStrategy;
        }

        return _cacheStrategy;
    }

    private string? SearchCore(ICacheStrategy cacheStrategy, byte[] ipBytes)
    {
        // Reset IO counter
        cacheStrategy.ResetIoCount();
        _cacheStrategy.ResetIoCount();
        _ipv6CacheStrategy?.ResetIoCount();

        // Number of bytes for each vector index item
        var vectorIndexSize = 8;

        // Number of columns indexed by vector
        var vectorIndexCols = 256;

        // Calculate the starting address of the vector index item.
        var il0 = ipBytes[0];
        var il1 = ipBytes[1];
        var idx = il0 * vectorIndexCols * vectorIndexSize + il1 * vectorIndexSize;

        var vector = cacheStrategy.GetVectorIndex(idx);
        var sPtr = BinaryPrimitives.ReadUInt32LittleEndian(vector.Span);
        var ePtr = BinaryPrimitives.ReadUInt32LittleEndian(vector.Span.Slice(4));

        var length = ipBytes.Length;
        var indexSize = length * 2 + 6;
        var l = 0;
        var h = (ePtr - sPtr) / indexSize;
        var dataLen = 0;
        long dataPtr = 0;

        while (l <= h)
        {
            int m = (int)(l + h) >> 1;

            var p = sPtr + m * indexSize;
            var buff = cacheStrategy.GetData(p, indexSize);

            var s = buff.Span.Slice(0, length);
            var e = buff.Span.Slice(length, length);
            if (ByteCompare(ipBytes, s) < 0)
            {
                h = m - 1;
            }
            else if (ByteCompare(ipBytes, e) > 0)
            {
                l = m + 1;
            }
            else
            {
                dataLen = BinaryPrimitives.ReadUInt16LittleEndian(buff.Span.Slice(length * 2, 2));
                dataPtr = BinaryPrimitives.ReadUInt32LittleEndian(buff.Span.Slice(length * 2 + 2, 4));
                break;
            }
        }

        var regionBuff = cacheStrategy.GetData(dataPtr, dataLen);
        return Encoding.UTF8.GetString(regionBuff.Span.ToArray());
    }

    static int ByteCompare(byte[] ip1, ReadOnlySpan<byte> ip2) => ip1.Length == 4 ? IPv4Compare(ip1, ip2) : IPv6Compare(ip1, ip2);

    static int IPv4Compare(byte[] ip1, ReadOnlySpan<byte> ip2)
    {
        var ret = 0;
        for (int i = 0; i < ip1.Length; i++)
        {
            var ip2Index = ip1.Length - 1 - i;
            if (ip1[i] < ip2[ip2Index])
            {
                return -1;
            }
            else if (ip1[i] > ip2[ip2Index])
            {
                return 1;
            }
        }
        return ret;
    }

    static int IPv6Compare(byte[] ip1, ReadOnlySpan<byte> ip2)
    {
        var ret = 0;
        for (int i = 0; i < ip1.Length; i++)
        {
            if (ip1[i] < ip2[i])
            {
                return -1;
            }
            else if (ip1[i] > ip2[i])
            {
                return 1;
            }
        }
        return ret;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        _cacheStrategy.Dispose();
        _ipv6CacheStrategy?.Dispose();
        GC.SuppressFinalize(this);
    }
}
