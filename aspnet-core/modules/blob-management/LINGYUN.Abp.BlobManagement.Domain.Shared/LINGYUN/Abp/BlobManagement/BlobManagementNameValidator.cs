using System;
using System.IO;
using Volo.Abp;

namespace LINGYUN.Abp.BlobManagement;

/// <summary>
/// BlobManagement name verification
/// </summary>
public static class BlobManagementNameValidator
{
    /// <summary>
    /// Characters that are not allowed (the union of illegal characters on each platform, including path separators)
    /// </summary>
    public static readonly char[] InvalidNameCharacters =
    [
        '\0', '/', '\\', ':', '*', '?', '"', '<', '>', '|',
    ];

    /// <summary>
    /// Verify a single name (container name, file/directory name), and throw an exception if it is illegal
    /// </summary>
    /// <remarks>
    /// Path separators are not allowed, otherwise container aliases will be generated or levels will be written into the name;
    /// Check in sequence: null value → traversal segment (./..) → illegal characters → trailing dot/space → retain device name.
    /// </remarks>
    public static void CheckName(string? name, string errorCode)
    {
        if (!IsValidSegment(name))
        {
            throw CreateInvalidNameException(name, errorCode);
        }
    }

    /// <summary>
    /// Verify the name path (such as <see cref="Blob.FullName"/>), and throw an exception if it is illegal
    /// </summary>
    /// <remarks>
    /// Allow '/' and '\' as delimiters, but verify each section according to the single name rule;
    /// First remove the leading '/' (the full name of the module is agreed to start with '/'), and then reject absolute paths and UNC.
    /// </remarks>
    public static void CheckPath(string? path, string errorCode)
    {
        if (!IsValidPath(path))
        {
            throw CreateInvalidNameException(path, errorCode);
        }
    }

    /// <summary>
    /// Whether it is a legal path (check segment by segment, delimiters allowed)
    /// </summary>
    public static bool IsValidPath(string? path)
    {
        if (path.IsNullOrWhiteSpace())
        {
            return false;
        }

        var name = path.RemovePreFix("/");

        if (Path.IsPathRooted(name) ||
            name.StartsWith(@"\\", StringComparison.Ordinal))
        {
            return false;
        }

        var segments = name.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
        if (segments.Length == 0)
        {
            return false;
        }

        foreach (var segment in segments)
        {
            if (!IsValidSegment(segment))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Is it a legal single name (without separator)
    /// </summary>
    public static bool IsValidName(string? name)
    {
        return IsValidSegment(name);
    }

    /// <summary>
    /// Is it a legal path segment?
    /// </summary>
    public static bool IsValidSegment(string? segment)
    {
        if (segment.IsNullOrWhiteSpace())
        {
            return false;
        }
        if (segment == "." || segment == "..")
        {
            return false;
        }
        if (segment.IndexOfAny(InvalidNameCharacters) >= 0)
        {
            return false;
        }
        if (segment.EndsWith(".") || segment.EndsWith(" "))
        {
            return false;
        }

        return !IsReservedDeviceName(segment);
    }

    /// <summary>
    /// Whether to reserve device names for Windows (CON/PRN/AUX/NUL/COM1-9/LPT1-9)
    /// </summary>
    public static bool IsReservedDeviceName(string name)
    {
        if (name.IsNullOrWhiteSpace())
        {
            return false;
        }

        var extensionIndex = name.IndexOf('.');
        if (extensionIndex > 0)
        {
            name = name.Substring(0, extensionIndex);
        }

        if (name.Equals("CON", StringComparison.OrdinalIgnoreCase) ||
            name.Equals("PRN", StringComparison.OrdinalIgnoreCase) ||
            name.Equals("AUX", StringComparison.OrdinalIgnoreCase) ||
            name.Equals("NUL", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (name.Length == 4)
        {
            var prefix = name.Substring(0, 3);
            var suffix = name[3];

            return suffix >= '1' && suffix <= '9' &&
                   (prefix.Equals("COM", StringComparison.OrdinalIgnoreCase) ||
                    prefix.Equals("LPT", StringComparison.OrdinalIgnoreCase));
        }

        return false;
    }

    /// <summary>
    /// Create an illegal name exception
    /// </summary>
    public static BusinessException CreateInvalidNameException(string? name, string errorCode)
    {
        return new BusinessException(errorCode, $"Not allowed name: {name}!")
            .WithData("Name", name ?? string.Empty);
    }
}
