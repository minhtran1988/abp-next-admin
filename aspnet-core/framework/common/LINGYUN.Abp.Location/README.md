# LINGYUN.Abp.Location

## Introduction

`LINGYUN.Abp.Location` is a basic location service module that provides geographical location-related functions, including geocoding (forward/reverse), distance calculation and other functions.

## Features

* Geocoding and Reverse Geocoding
* IP geographical location analysis
* Position distance calculation (based on Google algorithm, error <0.2 meters)
* Position offset calculation
* Support POI (point of interest) and road information
* Extensible location resolution provider

## Installation

```bash
dotnet add package LINGYUN.Abp.Location
```

## How to use

1. Add module dependencies:

```csharp
[DependsOn(typeof(AbpLocationModule))]
public class YourModule : AbpModule
{
    // ...
}
```

2. Inject and use the location resolution service:

```csharp
public class YourLocationService
{
    private readonly ILocationResolveProvider _locationProvider;

    public YourLocationService(ILocationResolveProvider locationProvider)
    {
        _locationProvider = locationProvider;
    }

    //Geocoding: address to coordinates
    public async Task<GecodeLocation> GeocodeAsync(string address)
    {
        return await _locationProvider.GeocodeAsync(address);
    }

    // Reverse geocoding: coordinates to address
    public async Task<ReGeocodeLocation> ReGeocodeAsync(double lat, double lng)
    {
        return await _locationProvider.ReGeocodeAsync(lat, lng);
    }

    //IP geographical location analysis
    public async Task<IPGecodeLocation> IPGeocodeAsync(string ipAddress)
    {
        return await _locationProvider.IPGeocodeAsync(ipAddress);
    }
}
```

## Position calculation

The module provides powerful position calculation functions:

```csharp
//Create location object
var location1 = new Location { Latitude = 39.9042, Longitude = 116.4074 }; // Beijing
var location2 = new Location { Latitude = 31.2304, Longitude = 121.4737 }; // Shanghai

// Calculate the distance between two points (meters)
double distance = location1.CalcDistance(location2);

// Calculate the offset of the position
var offset = location1.CalcOffset(1000, 45); // Offset 1000 meters to the northeast
```

## Custom location resolution provider

To implement a custom location resolution provider, you need:

1. Implement the `ILocationResolveProvider` interface:

```csharp
public class CustomLocationProvider : ILocationResolveProvider
{
    public async Task<IPGecodeLocation> IPGeocodeAsync(string ipAddress)
    {
        // Implement IP geographical location analysis
    }

    public async Task<GecodeLocation> GeocodeAsync(string address, string city = null)
    {
        // Implement geocoding
    }

    public async Task<ReGeocodeLocation> ReGeocodeAsync(double lat, double lng, int radius = 50)
    {
        // Implement reverse geocoding
    }
}
```

2. Register your implementation in the module:

```csharp
public override void ConfigureServices(ServiceConfigurationContext context)
{
    context.Services.AddTransient<ILocationResolveProvider, CustomLocationProvider>();
}
```

## More information

* [English Documentation](./README.EN.md)
* [Baidu Map Location Service](./LINGYUN.Abp.Location.Baidu/README.md)
* [Tencent Map Positioning Service](./LINGYUN.Abp.Location.Tencent/README.md)
