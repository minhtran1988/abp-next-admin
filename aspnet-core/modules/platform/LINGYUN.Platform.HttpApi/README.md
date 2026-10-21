# LINGYUN.Platform.HttpApi

The HTTP API layer of the platform management module provides a RESTful style API interface.

## Features

* Menu management API
  * Get the current user menu
  * Get user menu list
  * Get character menu list
  * Menu CRUD operation
  * User favorite menu management

* Layout management API
  * Layout CRUD operations
  * Get a list of all layouts

* Data Dictionary API
  * Data dictionary CRUD operations
  * Data dictionary item management

* Package management API
  * Includes CRUD operations
  * Package file upload and download
  * Get the latest version package

## Module reference

```csharp
[DependsOn(typeof(PlatformHttpApiModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## API Controller

* `MenuController`: menu management controller
  * `GET /api/platform/menus/by-current-user`: Get the current user menu
  * `GET /api/platform/menus/by-user`: Get the specified user menu
  * `GET /api/platform/menus/by-role`: Get the role menu
  * `POST /api/platform/menus`: Create menu
  * `PUT /api/platform/menus/{id}`: update menu
  * `DELETE /api/platform/menus/{id}`: delete menu

* `UserFavoriteMenuController`: User favorite menu controller
  * `GET /api/platform/menus/favorites/my-favorite-menus`: Get my favorite menu
  * `POST /api/platform/menus/favorites/my-favorite-menus`: Create a favorite menu
  * `PUT /api/platform/menus/favorites/my-favorite-menus/{MenuId}`: Update favorite menu
  * `DELETE /api/platform/menus/favorites/my-favorite-menus`: delete favorite menu

* `PackageController`: package management controller
  * `GET /api/platform/packages/{Name}/latest`: Get the latest version package
  * `POST /api/platform/packages/{id}/blob`: Upload package file
  * `GET /api/platform/packages/{id}/blob/{Name}`: Download package file
  * `DELETE /api/platform/packages/{id}/blob/{Name}`: Delete package files

## Configuration items

```json
{
  "App": {
    "CorsOrigins": "https://*.YourDomain.com,http://localhost:4200"
  }
}
```

## More

For more information please refer to [Platform](../README.md)
