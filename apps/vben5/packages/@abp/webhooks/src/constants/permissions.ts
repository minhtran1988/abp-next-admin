/** Group permissions */
export const GroupDefinitionsPermissions = {
  /** Add new */
  Create: 'AbpWebhooks.GroupDefinitions.Create',
  Default: 'AbpWebhooks.GroupDefinitions',
  /** Delete */
  Delete: 'AbpWebhooks.GroupDefinitions.Delete',
  /** Update */
  Update: 'AbpWebhooks.GroupDefinitions.Update',
};

/** Webhook defines permissions */
export const WebhookDefinitionsPermissions = {
  /** Add new */
  Create: 'AbpWebhooks.Definitions.Create',
  Default: 'AbpWebhooks.Definitions',
  /** Delete */
  Delete: 'AbpWebhooks.Definitions.Delete',
  /** Update */
  Update: 'AbpWebhooks.Definitions.Update',
};

/** Webhook subscription permissions */
export const WebhookSubscriptionPermissions = {
  /** Add new */
  Create: 'AbpWebhooks.Subscriptions.Create',
  Default: 'AbpWebhooks.Subscriptions',
  /** Delete */
  Delete: 'AbpWebhooks.Subscriptions.Delete',
  /** Update */
  Update: 'AbpWebhooks.Subscriptions.Update',
};

/** Webhook sending record permission */
export const WebhooksSendAttemptsPermissions = {
  Default: 'AbpWebhooks.SendAttempts',
  /** Delete */
  Delete: 'AbpWebhooks.SendAttempts.Delete',
  /** Update */
  Resend: 'AbpWebhooks.SendAttempts.Resend',
};
