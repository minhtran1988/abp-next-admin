# LINGYUN.Abp.OpenIddict.QrCode

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2Fcolinin%2Fabp-next-admin%2Fmaster%2Faspnet-core%2Fmodules%2FopenIddict%2FLINGYUN.Abp.OpenIddict.QrCode%2FLINGYUN.Abp.OpenIddict.QrCode.csproj)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/LINGYUN.Abp.OpenIddict.QrCode.svg?style=flat-square)](https://www.nuget.org/packages/LINGYUN.Abp.OpenIddict.QrCode)

## Introduction

`LINGYUN.Abp.OpenIddict.QrCode` is OpenIddict's code scanning login authentication extension module, which provides the code scanning login authentication function.

[English](./README.EN.md)

## Features

*Scan the QR code to log in

* Security log
  * Log login attempts
  * Record login failure

## Installation

```bash
dotnet add package LINGYUN.Abp.OpenIddict.QrCode
```

## Use

1. Add `[DependsOn(typeof(AbpOpenIddictQrCodeModule))]` to your module class.

2. Configure OpenIddict server:

```csharp
public override void PreConfigureServices(ServiceConfigurationContext context)
{
    PreConfigure<OpenIddictServerBuilder>(builder =>
    {
        // Allow portal authentication process
        builder.AllowQrCodeFlow();
    });
}
```

3. Usage example:

```http
POST /connect/token
Content-Type: application/x-www-form-urlencoded

grant_type=qr_code&
qrcode_key=a73faf8936f649868223a9287317d8d1&
scope=openid profile
```

## Certification process

1. QR code verification
   * User provides QR code Key (qrcode_key_)
   * If not provided or invalid, an invalid QR code error will be returned.

## Parameter description

* qrcode_key_ (required)
  * QR code Key

## Notes

* All authentication operations will be recorded in security logs
* It is recommended to use HTTPS in production environments
