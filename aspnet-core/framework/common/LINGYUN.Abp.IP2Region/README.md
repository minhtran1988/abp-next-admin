# LINGYUN.Abp.IP2Region

## Introduction

`LINGYUN.Abp.IP2Region` is an ABP framework module based on IP2Region, providing IP address query function. This module integrates the IP2Region.Net library and provides convenient IP address query services.

## Function

* Provide IP address query service
* Supports both IPv4 and IPv6 offline libraries, automatically selects data files according to address family
* Supports multiple caching strategies
* Built-in IPv4 and IPv6 database files
* Support ABP virtual file system

## Installation

```bash
dotnet add package LINGYUN.Abp.IP2Region
```

## Use

1. Add `[DependsOn(typeof(AbpIP2RegionModule))]` to your module class.

```csharp
[DependsOn(typeof(AbpIP2RegionModule))]
public class YourModule : AbpModule
{
    // ...
}
```

2. Inject and use the IP query service:

```csharp
public class YourService
{
    private readonly ISearcher _searcher;

    public YourService(ISearcher searcher)
    {
        _searcher = searcher;
    }

    public string? SearchIpInfo(string ip)
    {
        // Automatically select IPv4 / IPv6 offline library by address family
        return _searcher.Search(ip);
    }
}
```

## IPv4 / IPv6 support

The module has two built-in offline library files (loaded through the ABP virtual file system):

| Offline library | Path |
|---|---|
| IPv4 | `/LINGYUN/Abp/IP2Region/Resources/ip2region_v4.xdb` |
| IPv6 | `/LINGYUN/Abp/IP2Region/Resources/ip2region_v6.xdb` |

`ISearcher` registers `AbpSearcher`, loads two offline libraries at the same time and **automatically routes by address family**:

* IPv4 address query IPv4 offline library, IPv6 address query IPv6 offline library;
* IPv4-mapped IPv6 addresses (such as `::ffff:1.2.3.4`) will be normalized to IPv4 queries;
* When the IPv6 offline library resource does not exist (for example, excluded during packaging), the IPv6 query returns `null`, and the IPv4 offline library will not be used to parse out error results.

You can also use `AbpSearcher` directly:

```csharp
//Single file mode: only load one offline library, and the caller ensures that the query address is consistent with the offline library version
using var v6Searcher = new AbpSearcher(CachePolicy.File, ipv6XdbStream);

//Dual file mode: load IPv4 and IPv6 offline libraries at the same time, automatically route according to address family
using var searcher = new AbpSearcher(CachePolicy.File, ipv4XdbStream, ipv6XdbStream);

var region = searcher.Search("2400:3200::1"); // China|Zhejiang Province|Hangzhou City|Alibaba|CN
```

> The IPv6 offline library (about 35MB) is provided with the package and can be excluded during packaging through `-p:IncludeIPv6Database=false` to reduce the size.

## Options

```csharp
Configure<AbpIP2RegionOptions>(options =>
{
    // Offline library files: physical files first, followed by virtual file system paths (built-in resources)
    options.IPv4DatabaseFile = "/data/ip2region_v4.xdb";
    options.IPv6DatabaseFile = "/data/ip2region_v6.xdb";
    //Default value: CachePolicy.Content (concurrency safety)
    options.CachePolicy = CachePolicy.Content;
});
```

```json
{
  "IP2Region": {
    "IPv4DatabaseFile": "/data/ip2region_v4.xdb",
    "IPv6DatabaseFile": "/data/ip2region_v6.xdb",
    "CachePolicy": "Content"
  }
}
```

> The IPv4/IPv6 offline library uses built-in resources by default; when configured as a physical file path, ** physical files are loaded first **, making it easy to replace/update the offline library
> (The offline library can be downloaded from [ip2region](https://github.com/lionsoul2014/ip2region) or generated using maker);
> IPv6 offline library is optional. When the file does not exist, IPv6 query returns `null`.

### Caching strategy

| Cache Strategy | Description | Concurrency Safety |
|---|---|---|
| `Content` (default) | All offline libraries are loaded into memory, and singleton queryers can be used concurrently | Yes |
| `VectorIndex` | Only cache the vector index (512KB), the rest reads the data stream | No |
| `File` | Completely based on file stream query, minimal memory usage | No |

> `ISearcher` is registered as a singleton, and `VectorIndex`/`File` is the implementation of "shared data flow + Seek". Multi-threaded concurrent queries will interrupt each other's data flow positioning.
> Only used when serial query can be guaranteed; memory usage reference: IPv4 library is about 10MB, IPv6 library is about 35MB.

## Remarks rules

Remarks rules are processed uniformly by `LINGYUN.Abp.IP.Location` (other location resolution modules, such as MaxMind, share the same configuration):

```csharp
Configure<AbpIPLocationResolveOptions>(options =>
{
    //The matched country name is removed from the remarks (only Chinese IP does not display the country)
    options.RemoveCountry = new[] { "\u4e2d\u56fd" };
    //The matched province name is removed from the remarks
    // options.RemoveProvince = new[] { "\u9999\u6e2f\u7279\u522b\u884c\u653f\u533a" };
});
```

You can also use the configuration section directly (the `LINGYUN.Abp.IP.Location` module binds the `IPLocation` section):

```json
{
  "IPLocation": {
    "RemoveCountry": [ "\u4e2d\u56fd" ]
  }
}
```

```yaml
# docker-compose environment
- "IPLocation__RemoveCountry__0=\u4e2d\u56fd"
```

> The ip2region database returns Chinese country, province and city names. The configuration examples
> above use `\u4e2d\u56fd` (China) and `\u9999\u6e2f\u7279\u522b\u884c\u653f\u533a` (Hong Kong SAR); the English names
> in the examples below are shown for readability only.

* When the country name matches `RemoveCountry`, use the province and city in the remarks (such as `Hangzhou City, Zhejiang Province`; when the province and city have the same name, only the city is displayed, such as `Chongqing City`; when there are only provinces, it is `Hong Kong Special Administrative Region`);
* When the province name matches `RemoveProvince`, only use the city (such as `Hangzhou City`) for remarks;
* When there is no match, the remark only displays the country name (such as `China`, `Japan`); when the matched name is removed and there is no level to display, the remark is empty;
* Name matching ignores case and leading and trailing whitespace, and is applicable to IP address resolution in all countries.

## Special address processing

* **Illegal addresses, local loopbacks and unspecified addresses** (such as `not-an-ip`, `127.0.0.1`, `::1`, `0.0.0.0`, `::`) are handled by `IIPLocationResolver`
  Unified processing, empty parsing results are returned directly (`Location` is `null`), and the location parsing module will not be entered;
* **Intranet, link local, multicast, reserved address, etc.** are returned in English `Reserved` in the ip2region offline library.
  The module returns an `IPLocation` of `Country`/`Province`/`City`/`Remarks` **all empty**:

| Address Classification | Example |
|---|---|
| Intranet | `10.0.0.1`, `172.16.0.1`, `192.168.1.1`, `fd00::1` |
| link local | `169.254.1.1`, `fe80::1` |
| Carrier Grade NAT | `100.64.0.1` |
| Documentation examples/Benchmarks | `192.0.2.1`, `2001:db8::1`, `198.18.0.1` |
| Multicast/Broadcast | `224.0.0.1`, `ff02::1`, `255.255.255.255` |
| Reserved (Other address segments of the offline library mark `Reserved`) | `64:ff9b::1` |

## IP2Region.Net library description

### Installation

Install the package with [NuGet](https://www.nuget.org/packages/IP2Region.Net)

```bash
Install-Package IP2Region.Net
```

### Usage

```csharp
using IP2Region.Net.Abstractions;
using IP2Region.Net.XDB;

ISearcher searcher = new Searcher(CachePolicy , "your xdb file path");
```

### Cache Policy Description
| Cache Policy            | Description                                                                                                | Thread Safe |
|-------------------------|------------------------------------------------------------------------------------------------------------|-------------|
| CachePolicy.Content     | Cache the entire `xdb` data.                                                                               | Yes         |
| CachePolicy.VectorIndex | Cache `vecotorIndex` to speed up queries and reduce system io pressure by reducing one fixed IO operation. | Yes         |
| CachePolicy.File        | Completely file-based queries                                                                              | Yes         |
### XDB File Description
Generate using [maker](https://github.com/lionsoul2014/ip2region/tree/master/maker/csharp), or [download](https://github.com/lionsoul2014/ip2region/blob/master/data/ip2region.xdb) pre-generated xdb files

## ASP.NET Core Usage

```csharp
services.AddSingleton<ISearcher>(new Searcher(CachePolicy , "your xdb file path"));
```

## Performance

``` ini

BenchmarkDotNet=v0.13.2, OS=macOS 13.4.1 (c) (22F770820d) [Darwin 22.5.0]
Apple M1, 1 CPU, 8 logical and 8 physical cores
.NET SDK=7.0.306
  [Host]     : .NET 6.0.20 (6.0.2023.32017), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.20 (6.0.2023.32017), Arm64 RyuJIT AdvSIMD


```
| Method                  |       Mean |    Error |   StdDev |
|-------------------------|-----------:|---------:|---------:|
| CachePolicy_Content     |   155.7 ns |  0.46 ns |  0.39 ns |
| CachePolicy_File        | 2,186.8 ns | 34.27 ns | 32.06 ns |
| CachePolicy_VectorIndex | 1,570.3 ns | 27.53 ns | 22.99 ns |

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[Apache License 2.0](https://github.com/lionsoul2014/ip2region/blob/master/LICENSE.md)

## Link

* [English document](./README.EN.md)