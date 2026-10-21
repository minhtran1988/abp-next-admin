# LINGYUN.Abp.Location.Tencent

## Introduction

`LINGYUN.Abp.Location.Tencent` is a location service implementation module based on Tencent Map API, which provides geocoding, reverse geocoding, IP positioning and other functions.

## Features

* Geocoding: Convert detailed structured addresses into corresponding latitude and longitude coordinates
* Reverse geocoding: convert latitude and longitude coordinates into corresponding structured addresses
* IP positioning: Obtain location information based on IP address
* POI (point of interest) information: Get information on surrounding shops, restaurants and other points of interest
* Administrative division information: obtain detailed administrative division level information
* Address resolution: Intelligent resolution of address information, supporting multiple formats

## Installation

```bash
dotnet add package LINGYUN.Abp.Location.Tencent
```

## Configuration

1. Add module dependencies:

```csharp
[DependsOn(typeof(AbpTencentLocationModule))]
public class YourModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TencentLocationOptions>(options =>
        {
            //Set Tencent Map API key
            options.Key = "your-tencent-map-key";
            // Optional: Set security key (SK verification)
            options.SecretKey = "your-tencent-map-sk";
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
    "title": "Tiananmen", // place name
    "address": "East Chang'an Street, Dongcheng District, Beijing", // address
    "category": "tourist attractions", // category
    "adcode": "110101", // Administrative division code
    "similarity": 0.8, // Similarity (0-1)
    "reliability": 7, // Reliability (1-10)
    "level": 11 //Address type
}
```

### Reverse geocoding return data

```json
{
    "location": {
        "lat": 39.915119, // latitude value
        "lng": 116.403963 // longitude value
    },
    "address": "East Chang'an Street, Dongcheng District, Beijing", // full address
    "formatted_addresses": {
        "recommend": "Dongcheng District Tiananmen", // Recommended address
        "rough": "Dongcheng District, Beijing" // Rough address
    },
    "address_component": {
        "nation": "China", // country
        "province": "Beijing City", // province
        "city": "Beijing", // city
        "district": "Dongcheng District", // district and county
        "street": "East Chang'an Street", // street
        "street_number": "No. 1" // House number
    },
    "pois": [ // Surrounding POI information
        {
            "title": "Tiananmen", // POI name
            "address": "East Chang'an Street, Dongcheng District, Beijing", // POI address
            "category": "Tourist Attractions", // POI type
            "distance": 100, // distance (meters)
            "_distance": 100.0, // distance (meters, floating point number)
            "tel": "", // Telephone
            "ad_info": { // Administrative division information
                "adcode": "110101", // Administrative division code
                "name": "Dongcheng District", // Administrative division name
                "location": { // Administrative division center point
                    "lat": 39.915119,
                    "lng": 116.403963
                }
            }
        }
    ]
}
```

## More information

* [English Documentation](./README.EN.md)
* [Tencent Location Service](https://lbs.qq.com/)
