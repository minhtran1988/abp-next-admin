interface NotificationItem {
  id: number | string;
  avatar: string;
  date: string;
  isRead?: boolean;
  message: string;
  title: string;
  /**
   * Jump link, which can be a routing path or a complete URL
   * @example '/dashboard' or 'https://example.com'
   */
  link?: string;
  query?: Record<string, any>;
  state?: Record<string, any>;
}

export type { NotificationItem };
