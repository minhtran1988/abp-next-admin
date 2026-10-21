import type { PagedAndSortedResultRequestDto } from '@abp/core';

import type {
  NotificationContentType,
  NotificationData,
  NotificationLifetime,
  NotificationReadState,
  NotificationSeverity,
  NotificationType,
} from './notifications';

interface GetMyNotifilerPagedListInput extends PagedAndSortedResultRequestDto {
  filter?: string;
  /** Read status */
  readState?: NotificationReadState;
}

interface MarkReadStateInput {
  idList: string[];
  state: NotificationReadState;
}

interface UserNotificationDto {
  /** Content type */
  contentType: NotificationContentType;
  /** Creation time */
  creationTime: Date;
  /** data */
  data: NotificationData;
  /** Id */
  id: string;
  /** life cycle */
  lifetime: NotificationLifetime;
  /** name */
  name: string;
  /** Urgency */
  severity: NotificationSeverity;
  /** Reading status */
  state: NotificationReadState;
  /** Type */
  type: NotificationType;
}

export type {
  GetMyNotifilerPagedListInput,
  MarkReadStateInput,
  UserNotificationDto,
};
