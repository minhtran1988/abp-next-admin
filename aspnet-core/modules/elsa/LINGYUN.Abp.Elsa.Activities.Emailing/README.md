# LINGYUN.Abp.Elsa.Activities.Emailing

Email sending activity integration module for Elsa workflow

## Function

* Provides **SendEmailing** activity for sending emails
  *Supports sending to multiple recipients
  * Supports using templates to render email content
  * Support JavaScript and Liquid syntax
  * Integrate the `IEmailSender` interface of the ABP framework
  * Integrate the `ITemplateRenderer` interface of the ABP framework for template rendering

## Configuration usage

```csharp
[DependsOn(
    typeof(AbpElsaActivitiesEmailingModule)
    )]
public class YouProjectModule : AbpModule
{
}
```

## appsettings.json

```json
{
    "Elsa": {
        "Emailing": true // Enable email sending activity
    }
}
```

## Activity parameters

* **To**: Recipient email address list
* **Subject**: Email subject
* **Body**: Email body content
* **Culture**: Cultural information
* **Template**: template name
* **Model**: Model parameters used to format template content

[English](./README.EN.md)
