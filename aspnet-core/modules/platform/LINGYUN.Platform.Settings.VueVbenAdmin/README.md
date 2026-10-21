# LINGYUN.Platform.Settings.VueVbenAdmin

The platform setting module of the VueVbenAdmin front-end framework provides theme, layout, menu and other setting functions.

## Features

* Theme settings
  * Dark mode
  * Gray mode
  * Color weak mode
  * theme color

* Layout settings
  * Full screen mode
  * Content mode
  * Page loading status
  * Footer display
  * Back to top

* Menu settings
  * Menu mode
  * Menu theme
  * Menu width
  * Menu collapse
  * Menu split
  * Menu drag and drop

* Title bar settings
  * Fixed title bar
  * Title bar theme
  * Full screen button
  * Document button
  * Notification button
  *Search button

*Multi-tab settings
  *Tab cache
  *Tab drag and drop
  *Tab page refresh
  *Tab folding

## Module reference

```csharp
[DependsOn(typeof(PlatformSettingsVueVbenAdminModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Localized resources

*Support simplified Chinese
* Support English

## Configuration items

```json
{
  "Settings": {
    "DarkMode": false,
    "PageLoading": true,
    "PermissionCacheType": 1,
    "ShowSettingButton": true,
    "ShowDarkModeToggle": true,
    "SettingButtonPosition": "auto",
    "PermissionMode": "ROUTE_MAPPING",
    "SessionTimeoutProcessing": 0,
    "GrayMode": false,
    "ColorWeak": false,
    "ThemeColor": "#0960bd",
    "FullContent": false,
    "ContentMode": "full",
    "ShowLogo": true,
    "ShowFooter": true,
    "OpenKeepAlive": true,
    "LockTime": 0,
    "ShowBreadCrumb": true,
    "ShowBreadCrumbIcon": false
  }
}
```

## More

For more information please refer to [Platform](../README.md)
