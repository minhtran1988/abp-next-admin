# LINGYUN.Abp.MaxMindGeoIP2

## Introduction

`LINGYUN.Abp.MaxMindGeoIP2` is an ABP framework module based on [MaxMind.GeoIP2](https://github.com/maxmind/GeoIP2-dotnet),
Parse the IP geographical location through the mmdb offline library (to be provided by the user, see below), and register the location resolution contributor of `LINGYUN.Abp.IP.Location`,
Can be used interchangeably with `LINGYUN.Abp.IP2Region` (remarks rules remain the same).

## Function

* Parse IP geographical location (country/province/city) through mmdb offline library
* **No built-in offline library**: GeoLite2 is subject to the MaxMind license and cannot be distributed with the package. It needs to point to the mmdb file downloaded by the user through `MaxMindGeoIP2:DatabaseFile`
* Use `EmptyGeoIP2DatabaseReader` when the offline library file does not exist (parse empty content, empty position and record warning log), which will not block business
* Supports both IPv4 and IPv6 (depends on offline library)
* Parse geographical names according to the current culture (obtained from `Names`, fallback to `Name` if missing)
* Local loopback, intranet, reserved addresses and other unrecorded addresses do not return data (remarks are empty)
* Support ABP virtual file system

## Installation

```bash
dotnet add package LINGYUN.Abp.MaxMindGeoIP2
```

## Use

1. Add `[DependsOn(typeof(AbpMaxMindGeoIP2Module))]` to your module class.

```csharp
[DependsOn(typeof(AbpMaxMindGeoIP2Module))]
public class YourModule : AbpModule
{
    // ...
}
```

2. Inject `IIPLocationResolver` to resolve IP location:

```csharp
public class YourService
{
    private readonly IIPLocationResolver _ipLocationResolver;

    public YourService(IIPLocationResolver ipLocationResolver)
    {
        _ipLocationResolver = ipLocationResolver;
    }

    public async Task<string?> ResolveAsync(string ip)
    {
        var result = await _ipLocationResolver.ResolveAsync(ip);

        // 223.5.5.5 => Hangzhou, Zhejiang
        return result.Location?.Remarks;
    }
}
```

## Options

```csharp
Configure<AbpMaxMindGeoIP2Options>(options =>
{
    // Offline library file path (required): You need to download it from MaxMind and accept its license agreement
    options.DatabaseFile = "/data/GeoLite2-City.mmdb";
    //File opening method, default memory mapping
    options.FileAccessMode = FileAccessMode.MemoryMapped;
});
```

```json
{
  "MaxMindGeoIP2": {
    "DatabaseFile": "/data/GeoLite2-City.mmdb",
    "FileAccessMode": "MemoryMapped"
  }
}
```

### Geographical Names and Culture

The query does not specify locales (that is, it is opened with the default name of the offline library `Name`), and the geographical name is parsed according to the **current culture**
(`CultureInfo.CurrentUICulture`) Obtained from MaxMind's `Names` dictionary:

* First match by the complete culture name (such as `zh-CN`), and then match by the language name if no hits are found (such as `es-MX` → `es`);
* Fall back to `Name` (offline library default language, usually English) when still not hit;
* The GeoLite2-City offline library provides `de,en,es,fr,ja,pt-BR,ru,zh-CN` languages, and some entities only provide a few languages (in this case, fallback according to the above rules).

So the same singleton queryer can serve all cultures, eliminating the need to create multiple queryers per language.

| Current culture | Notes for `8.8.8.8` | Notes for `223.5.5.5` |
|---|---|---|
| `zh-CN` | United States | Hangzhou, Zhejiang |
| `en-US` | United States | China |
| `ja-JP` | アメリカ | China |
| `ko-KR` (the language is not available in the offline library, fallback to `Name`) | United States | China |

### Remarks rules

Remarks rules are processed uniformly by `LINGYUN.Abp.IP.Location` and share the same configuration with `LINGYUN.Abp.IP2Region`:

```csharp
Configure<AbpIPLocationResolveOptions>(options =>
{
    //The matched country name is removed from the remarks (only Chinese IP does not display the country)
    options.RemoveCountry = new[] { "China" };
    //The matched province name is removed from the remarks
    // options.RemoveProvince = new[] { "Hong Kong Special Administrative Region" };
});
```

You can also use the configuration section directly (the `LINGYUN.Abp.IP.Location` module binds the `IPLocation` section):

```json
{
  "IPLocation": {
    "RemoveCountry": [ "China" ]
  }
}
```

```yaml
# docker-compose environment
- IPLocation__RemoveCountry__0=China
```

| Scenario | `Remarks` Example |
|---|---|
| The country name matches `RemoveCountry`, and there are provinces and cities | `Zhejiang Hangzhou` (only the city is displayed when the province and city have the same name, such as `Chongqing City`) |
| Country name matches `RemoveCountry`, and only provinces | `Hong Kong SAR` |
| Country name matches `RemoveCountry`, and only country data | empty |
| Province name matching `RemoveProvince` | `Hangzhou` (only use city) |
| None matched | `United States`, `Japan`, `Germany` (only country names) |
| Local loopback / Intranet / Reserved / Not included in the offline library | Empty |

> Name matching ignores case and leading and trailing whitespace, and is suitable for IP address resolution in all countries.

## Performance Benchmark

Measured using [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet)(`MemoryDiagnoser` + `InProcessEmitToolchain`):

```ini
BenchmarkDotNet v0.15.8, Windows 10.0.26100
Intel Core i7-14700K (20C/28T), .NET 10.0.12 (SDK 10.0.401), X64 RyuJIT x86-64-v3
GC=Concurrent Workstation, Toolchain=InProcessEmitToolchain (IterationCount=15)
```

| Method | Mean | Error | StdDev | Ratio | Gen0 | Gen1 | Allocated |
|---|---:|---:|---:|---:|---:|---:|---:|
| `DatabaseReader_TryCity_IPv4` (baseline) | 3.428 μs | 0.0415 μs | 0.0388 μs | 1.00 | 0.4807 | 0.0038 | 8.15 KB |
| `DatabaseReader_TryCity_IPv6` | 3.496 μs | 0.0273 μs | 0.0228 μs | 1.02 | 0.4807 | 0.0038 | 8.15 KB |
| `DatabaseReader_TryCity_String_IPv4` | 3.428 μs | 0.0611 μs | 0.0542 μs | 1.00 | 0.4845 | 0.0038 | 8.19 KB |
| `ResolveAsync_IPv4` (module full link) | 3.753 μs | 0.0426 μs | 0.0399 μs | 1.09 | 0.5112 | 0.0076 | 8.73 KB |
| `ResolveAsync_IPv6` (module full link) | 3.812 μs | 0.0364 μs | 0.0323 μs | 1.11 | 0.5188 | 0.0076 | 8.78 KB |

* **A single query takes about 3.4 μs** (single-thread theoretical throughput is about 290,000 times/second), IPv6 is basically the same as IPv4 (`Ratio` 1.02);
* There is no difference between `TryCity(IPAddress)` and `TryCity(string)`, and the string parsing overhead is negligible;
* The **full link** via `IIPLocationResolver` (DI Scope + Contributors + Parsing Names by Current Culture) only adds about **9%~11%**;
* Each query is allocated about **8.15 KB** (`CityResponse` model object graph), and the complete link is about 8.73 KB - it is recommended to pay attention to GC in high concurrency scenarios (for example, caching results for hotspot IPs);
* Measurement scenario: `TryCity` uses **physical mmdb + memory mapping** (`FileAccessMode.MemoryMapped`, the recommended deployment method); `ResolveAsync` uses the module default configuration (configured offline library + `IIPLocationResolver`);
* The benchmark uses the in-process tool chain (the current environment cannot derive an independent benchmark process), the values will be slightly better than the independent process mode, and are only used for horizontal comparison.

## mmdb offline library description

* **This module does not have a built-in offline library**: GeoLite2 database is subject to [MaxMind End User License Agreement (EULA)](https://www.maxmind.com/en/geolite2/eula)
  Constraint, it cannot be distributed with the package, so the user needs to download it by himself and specify the file path through `MaxMindGeoIP2:DatabaseFile`;
* When not configured (or the file does not exist), the module registers `EmptyGeoIP2DatabaseReader` - equivalent to parsing an empty content:
  `TryCity`/`TryCountry`, etc. return `false`, `City`/`Country`, etc. throw `AddressNotFoundException`
  (Same as when the real interpreter misses), the location analysis result is empty and a warning log is recorded, which does not affect the business process;
* GeoLite2 requires [MaxMind account](https://www.maxmind.com/en/geolite2/signup) and License Key to download and update regularly.
  In the production environment, it is recommended to point to the physical file through `DatabaseFile` and replace it regularly;
* Offline mmdb files can also be downloaded from the [wp-statistics/GeoLite2-City](https://github.com/wp-statistics/GeoLite2-City) repository:
  The compressed package `GeoLite2-City.mmdb.gz` is provided in the warehouse. After decompression, you get `GeoLite2-City.mmdb`.
  Then point to the file through `MaxMindGeoIP2:DatabaseFile`;
  Direct download address: <https://raw.githubusercontent.com/wp-statistics/GeoLite2-City/master/GeoLite2-City.mmdb.gz>
* GeoLite2-City supports both IPv4 and IPv6 and provides
  `de,en,es,fr,ja,pt-BR,ru,zh-CN` language;
* Illegal addresses, local loopbacks and unspecified addresses are handled uniformly by `IIPLocationResolver` (empty results are returned and the location resolution module is not entered);
* Addresses that are not recorded in the offline library (intranet, reserved, not included) return empty `IPLocation` (country/province/city and remarks are all empty);
* Query exceptions (such as offline library file corruption) will not block business processes: record warning logs and process them as no data (return to empty positions).

## Link

* [English document](./README.EN.md)
* [MaxMind GeoIP2 .NET](https://github.com/maxmind/GeoIP2-dotnet)
* [GeoLite2 offline library](https://dev.maxmind.com/geoip/geolite2-free-geolocation-data)
* [Offline mmdb download (wp-statistics/GeoLite2-City)](https://github.com/wp-statistics/GeoLite2-City)
