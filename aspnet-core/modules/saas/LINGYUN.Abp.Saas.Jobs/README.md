# LINGYUN.Abp.Saas.Jobs

The SaaS background job module provides background job implementation for tenant resource monitoring and management.

## Features

* Tenant usage monitoring job (TenantUsageMonitoringJob)
  * Monitor tenant resource usage
  * Handle tenant expiration warnings
  * Handle expired tenant resource recycling

## Configuration instructions

### Job configuration

```json
{
  "Hangfire": {
    "TenantUsageMonitoring": {
      "CronExpression": "0 0 * * *", // Execute once a day
      "Queue": "default", // job queue
      "Enabled": true // Whether to enable
    }
  }
}
```

### Job parameters

* Saas:AdminEmail: Administrator's email address, used to receive warning notifications
* Saas:TenantId: tenant identification

## More

[English](README.EN.md)
