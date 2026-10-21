# LINGYUN.Abp.Location.Baidu

## Introduction

`LINGYUN.Abp.Location.Baidu` is a location service implementation module based on Baidu Map API, which provides geocoding, reverse geocoding, IP positioning and other functions.

## Features

* Geocoding: Convert detailed structured addresses into corresponding latitude and longitude coordinates
* Reverse geocoding: convert latitude and longitude coordinates into corresponding structured addresses
* IP positioning: Obtain location information based on IP address
* POI (point of interest) information: Get information on surrounding shops, restaurants and other points of interest
* Road information: Get nearby road information
* Administrative division information: obtain detailed administrative division level information

## Installation

```bash
dotnet add package LINGYUN.Abp.Location.Baidu
```

## Configuration

1. Add module dependencies:

```csharp
[DependsOn(typeof(AbpBaiduLocationModule))]
public class YourModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<BaiduLocationOptions>(options =>
        {
            //Set Baidu Map API key
            options.AccessKey = "your-baidu-map-ak";
            // Optional: Set security key (sn verification)
            options.SecurityKey = "your-baidu-map-sk";
            // Optional: Set the coordinate system type (default is bd09ll)
            options.CoordType = "bd09ll";
            // Optional: set the output format (default is json)
            options.Output = "json";
        });
    }
}
```

## How to use

1. Inject and use the location resolution service:

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
        // The city parameter is optional and is used to specify the city where the address is located.
        return await _locationProvider.GeocodeAsync(address, "Beijing");
    }

    // Reverse geocoding: coordinates to address
    public async Task<ReGeocodeLocation> ReGeocodeAsync(double lat, double lng)
    {
        //The radius parameter is optional and specifies the search radius (meters)
        return await _locationProvider.ReGeocodeAsync(lat, lng, 1000);
    }

    //IP geographical location analysis
    public async Task<IPGecodeLocation> IPGeocodeAsync(string ipAddress)
    {
        return await _locationProvider.IPGeocodeAsync(ipAddress);
    }
}
```

## Return data description

### Geocoding return data

```json
{
    "location": {
        "lat": 39.915119, // latitude value
        "lng": 116.403963 // longitude value
    },
    "precise": 1, //Additional information about the location, whether to search accurately (1 is precise, 0 is imprecise)
    "confidence": 80, // Confidence
    "comprehension": 100, // Address understanding level
    "level": "door address" // address type
}
```

### Reverse geocoding return data

```json
{
    "location": {
        "lat": 39.915119, // latitude value
        "lng": 116.403963 // longitude value
    },
    "formatted_address": "East Chang'an Street, Dongcheng District, Beijing", // structured address information
    "business": "Tiananmen", // business district information
    "addressComponent": {
        "country": "China", // country
        "province": "Beijing City", // province
        "city": "Beijing", // city
        "district": "Dongcheng District", // district or county
        "street": "East Chang'an Street", // street
        "street_number": "No. 1" // House number
    },
    "pois": [ // Surrounding POI information
        {
            "name": "Tiananmen", // POI name
            "type": "tourist attractions", // POI type
            "distance": "100" // Distance (meters)
        }
    ],
    "roads": [ // Surrounding road information
        {
            "name": "East Chang'an Street", // road name
            "distance": "50" // Distance (meters)
        }
    ]
}
```

## More information

* [English Documentation](./README.EN.md)
* [Baidu Map Open Platform](https://lbsyun.baidu.com/)
