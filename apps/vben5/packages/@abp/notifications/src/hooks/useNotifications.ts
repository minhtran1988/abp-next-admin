import type { Notification, NotificationInfo } from '../types/notifications';

import { Events, useEventBus } from '@abp/core';
import { useSignalR } from '@abp/signalr';
import { notification } from 'ant-design-vue';

import {
  NotificationContentType,
  NotificationSeverity,
  NotificationType,
} from '../types/notifications';
import { useNotificationSerializer } from './useNotificationSerializer';

export function useNotifications() {
  const { deserialize } = useNotificationSerializer();
  const { publish, subscribe, unSubscribe } = useEventBus();
  const { init, off, on, onStart, stop } = useSignalR();

  /** Registration notification */
  function register() {
    _registerEvents();
    _init();
  }

  /** Release notification */
  function release() {
    _releaseEvents();
    stop();
  }

  function _init() {
    onStart(() => on(Events.GetNotification, _onNotifyReceived));
    init({
      autoStart: true,
      serverUrl: '/signalr-hubs/notifications',
      useAccessToken: true,
    });
  }

  /** Register notification event */
  function _registerEvents() {
    subscribe(Events.UserLogout, stop);
  }

  /** Release notification event */
  function _releaseEvents() {
    unSubscribe(Events.UserLogout, stop);
    off(Events.GetNotification, _onNotifyReceived);
  }

  /** Receive notification callback */
  function _onNotifyReceived(notificationInfo: NotificationInfo) {
    const notification = deserialize(notificationInfo);
    if (notification.type === NotificationType.ServiceCallback) {
      publish(notification.name, notification);
      return;
    }
    publish(Events.NotificationRecevied, notification);
    _notification(notification);
  }

  /** Notification push */
  function _notification(notifier: Notification) {
    let message = notifier.message;
    switch (notifier.contentType) {
      case NotificationContentType.Text: {
        if (notifier.description) {
          message = notifier.description;
        }
      }
    }
    switch (notifier.severity) {
      case NotificationSeverity.Error:
      case NotificationSeverity.Fatal: {
        notification.error({
          description: message,
          message: notifier.title,
        });
        break;
      }
      case NotificationSeverity.Info: {
        notification.info({
          description: message,
          message: notifier.title,
        });
        break;
      }
      case NotificationSeverity.Success: {
        notification.success({
          description: message,
          message: notifier.title,
        });
        break;
      }
      case NotificationSeverity.Warn: {
        notification.warning({
          description: message,
          message: notifier.title,
        });
        break;
      }
    }
  }

  return {
    register,
    release,
  };
}
