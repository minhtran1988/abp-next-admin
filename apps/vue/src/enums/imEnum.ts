export enum ChatEventEnum {
  /** User sends message */
  USER_SEND_MESSAGE = 'chat:user_send_message',
  /** User is online */
  USER_ONLINE = 'chat:user_online',
  /** User is offline */
  USER_OFFLINE = 'chat:user_offline',
  /** New friend application */
  USER_REQUEST_ADD_FRIEND = 'chat:user_request_friend',
  /** Group new user application */
  USER_REQUEST_ADD_GROUP = 'chat:user_request_group',
  /** User exits group */
  USER_EXIT_GROUP = 'chat:user_exit_group',
  /** New user message */
  USER_MESSAGE_NEW = 'chat:user_new_message',
  /** New group message */
  USER_MESSAGE_GROUP_NEW = 'chat:user_new_group_message',
  /** Read message */
  USER_MESSAGE_READ = 'chat:user_read_message',
  /** Withdraw message */
  USER_MESSAGE_RECALL = 'chat:user_recall_message',
}

export enum NotifyEventEnum {
  /** New notification */
  NOTIFICATIONS_RECEVIED = 'notifications:recevied',
  /** Notification read */
  NOTIFICATIONS_READ = 'notifications:read',
  /** Server callback */
  NOTIFICATIONS_SERVICE_CALLBACK = 'notifications:service_callback',
}
