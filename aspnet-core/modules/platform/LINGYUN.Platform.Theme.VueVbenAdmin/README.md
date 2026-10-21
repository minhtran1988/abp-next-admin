# LINGYUN.Platform.Theme.VueVbenAdmin

The theme management module of the VueVbenAdmin front-end framework provides management functions for themes, layouts, menus and other related configurations.

## Features

* Theme settings
  * Dark mode
  * Gray mode
  * Color weak mode
  * Theme color configuration

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
  * Fixed head
  * Header theme
  * Full screen button
  * Document button
  * Notification button
  *Search button

*Multi-tab settings
  *Tab cache
  *Tab drag and drop
  *Tab page refresh
  *Tab folding

## Quick start

1. Reference module
```csharp
[DependsOn(typeof(PlatformThemeVueVbenAdminModule))]
public class YouProjectModule : AbpModule
{
    // other
}
```

2. Configuration options
```json
{
  "Theme": {
    "DarkMode": false,
    "GrayMode": false,
    "ColorWeak": false,
    "ThemeColor": "#0960bd"
  },
  "Layout": {
    "FullContent": false,
    "ContentMode": "full",
    "ShowLogo": true,
    "ShowFooter": true,
    "ShowBreadCrumb": true,
    "ShowBreadCrumbIcon": false
  },
  "Menu": {
    "Mode": "inline",
    "Theme": "dark",
    "Width": 210,
    "Collapsed": false,
    "Split": false,
    "Draggable": true
  },
  "Header": {
    "Fixed": true,
    "Theme": "light",
    "ShowFullScreen": true,
    "ShowDoc": true,
    "ShowNotice": true,
    "ShowSearch": true
  },
  "MultiTab": {
    "Cache": true,
    "Draggable": true,
    "Refresh": true,
    "Fold": true
  }
}
```

## Instructions for use

1. Theme configuration
   *Support multiple theme mode switching
   * Customizable theme color
   * Provide theme persistent storage

2. Layout configuration
   * Flexible layout mode
   * Configurable page element display
   *Support responsive layout

3. Menu configuration
   * Various menu display modes
   * Support menu drag and drop sorting
   * Menu theme customization

4. Extension development
   * Support custom theme configuration provider
   * Extensible theme settings
   * Topic data localization support

## Notes

1. Theme configuration
   * Theme configuration will affect the interface display for all users
   * It is recommended to choose the appropriate theme mode according to actual needs

2. Performance considerations
   * Properly configure the number of tab caches
   * Use page loading animation appropriately

## More information

* [VueVbenAdmin theme configuration document](https://doc.vvbin.cn/guide/design.html)
* [ABP official document](https://docs.abp.io/)
