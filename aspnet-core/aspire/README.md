## Prerequisites

Please refer to [Aspire Docs](https://aspire.dev/get-started/prerequisites/) to check the required environment for operation.

## Install CLI

Please refer to [Aspire Docs](https://aspire.dev/get-started/install-cli/) to install Aspire CLI.

## Start Aspire

```shell
aspire run
```

## Notes

- Only for learning [Aspire](https://aspire.dev/docs/) Do not use in production environment!
- The project enables [Elasticsearch](https://aspire.dev/integrations/databases/elasticsearch/) by default as audit log and system operation log storage. Please modify the startup parameters as appropriate according to the local situation to avoid OOM!
- Multiple projects initialize Quartz at startup. Even if health checks are performed, database lock contention may still occur in some cases. Just restart the corresponding service in the dashboard.