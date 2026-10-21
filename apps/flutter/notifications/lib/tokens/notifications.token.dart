class NotificationTokens {
  /// Notify the producer
  static const String producer = "producer";
  /// Notify consumers
  static const String consumer = "consumer";
  /// Notify subscribers (receive notification data name, same name as Hub method), defined to facilitate subscription/cancellation
  static const String receiver = "get-notification";
}