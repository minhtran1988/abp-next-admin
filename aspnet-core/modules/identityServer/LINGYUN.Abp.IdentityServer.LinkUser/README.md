# LINGYUN.Abp.IdentityServer.LinkUser

IdentityServer user association module provides extended authorization type support for user association.

## Features

* Extended authorization validator
  * `LinkUserGrantValidator` - user association grant validator
    * Authorization type: `link_user`
    * Supports verifying access tokens
    * Supports verification of user association relationships
    * Support multi-tenant scenarios
    * Support custom statement extension

* Localization support
  * Built-in Chinese and English resources
  * Support expansion of other languages

## Module reference

```csharp
[DependsOn(
    typeof(AbpIdentityServerLinkUserModule)
)]
public class YourModule : AbpModule
{
    // ...
}
```

## Dependent modules

* `AbpIdentityServerDomainModule` - ABP IdentityServer domain module

## Configuration usage

### Authorization request parameters

* `grant_type` - must be `link_user`
* `access_token` - the current user's access token
* `LinkUserId` - the user ID to be linked
* `LinkTenantId` - the tenant ID of the user to be associated (optional)

### Authorization request example

```http
POST /connect/token
Content-Type: application/x-www-form-urlencoded

grant_type=link_user&
access_token=current_user_access_token&
LinkUserId=target_user_id&
LinkTenantId=target_tenant_id
```

### Custom declaration extension

```csharp
public class CustomLinkUserGrantValidator : LinkUserGrantValidator
{
    protected override Task AddCustomClaimsAsync(List<Claim> customClaims, IdentityUser user, ExtensionGrantValidationContext context)
    {
        //Add custom declaration
        customClaims.Add(new Claim("custom_claim", "custom_value"));

        return base.AddCustomClaimsAsync(customClaims, user, context);
    }
}
```

Related documents:
* [IdentityServer4 Documentation](https://identityserver4.readthedocs.io/)
* [ABP Identity Authentication Document](https://docs.abp.io/en/abp/latest/Authentication)

[View English document](README.EN.md)
