# dev_app

A new Flutter project.

## Getting Started

This project is a starting point for a Flutter application.

A few resources to get you started if this is your first Flutter project:

- [Lab: Write your first Flutter app](https://docs.flutter.dev/get-started/codelab)
- [Cookbook: Useful Flutter samples](https://docs.flutter.dev/cookbook)

For help getting started with Flutter development, view the
[online documentation](https://docs.flutter.dev/), which offers tutorials,
samples, guidance on mobile development, and a full API reference.

## Translations

### Pre-made localization documents
* [English](./translation.json)   
* [Simplified Chinese](./translation.zh-Hans.json)

### How to use

- local files

 **Replace the keys in [AbpTranslations](./lib/utils/localization.dart) with the following format: **

	```json
	{
		"zh-Hans": {
			"Center:Feedback": "Feedback",
			"Center:Help": "Online Help",
			"Center:Info": "About",
			...other localized content
		},
		"en": {
			"Center:Feedback": "Feedback",
			"Center:Help": "Help",
			"Center:Info": "Info",
			...other localizations,
		},
	}
	```
	
-Backend services

	**Copy the pre-made localization document to the localization directory of the background service. For details, see abp multi-language documentation**

## Environment environment configuration

> **application** application configuration
>> *name* application name

> **auth** Identity authentication server configuration
>> *<font color=red size=4>authority</font>* Identity authentication server address
>> *<font color=red size=4>clientId</font>* Client ID
>> *clientSecret* client secret

> **tenant** Multi-tenant configuration
>> *tenantKey* multi-tenant identification

> **localization** internationalization configuration
>> *defaultLanguage* Application default language

> **remoteServices** remote service configuration
>> <font color=red size=4>default</font> Default connection configuration <font color=red size=4>must be specified</font>
>>> url connection address
>>> rootNamespace root namespace (preserve configuration)

>> <font color=red size=4>avatar</font> Avatar interface configuration (if the user avatar is set to a relative path, the interface address prefix needs to be specified)
>>> url connection address

> **remoteEnvironment** Remote environment configuration (replaces the current environment configuration according to the given application policy)
>> *url* connection address (pull configuration information from this connection)
>> *method* request method
>> *headers* request headers
>> *strategy* application strategy, deepmerge (merge), overwrite (replacement)

> **notifications** Notification related settings
>> *android* Android notification settings (optional)
>>> - *channelId*: Channel ID (required after configuring the android node)
>>> - *channelName*: channel name (required after configuring the android node)
>>> - *channelDescription*: Channel description (optional)

>> *linux* Linux side notification settings (optional)
>>> - *defaultActionName*: Default click method name (must be lost after configuring a linux node)

>> *darwin* iOS/Mac os side notification settings (optional)


# Configuration example
[demo.json](./res/config/demo.json)
```json
{
    "application": {
        "name": "app-flutter"
    },
    "auth": {
        "clientId": "abp-flutter",
        "clientSecret": "1q2w3e*",
        "authority": "http://127.0.0.1:30000"
    },
    "tenant": {
        "tenantKey": "__tenant"
    },
    "localization": {
        "defaultLanguage": "zh-Hans"
    },
    "remoteServices": {
        "default": {
            "url": "http://127.0.0.1:30000"
        },
        "avatar": {
            "url": "http://127.0.0.1:30000/api/files/static/users/p/"
        }
    },
    "remoteEnvironment": {
        "url": "",
        "strategy": "deepmerge"
    },
    "notifications": {
        "serverUrl": "http://127.0.0.1:30000/signalr-hubs/notifications",
        "android": {
            "channelId": "abp-flutter",
            "channelName": "abp-flutter",
            "channelDescription": "Notification channel definition suitable for Android"
        },
        "linux": {
            "defaultActionName": "Open notification"
        }
    }
}
```