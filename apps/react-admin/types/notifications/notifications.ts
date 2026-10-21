import type { Dictionary } from "#/abp-core";

export enum NotificationLifetime {
	OnlyOne = 1,
	Persistent = 0,
}

export enum NotificationType {
	Application = 0,
	ServiceCallback = 30,
	System = 10,
	User = 20,
}

export enum NotificationContentType {
	Html = 1,
	Json = 3,
	Markdown = 2,
	Text = 0,
}

export enum NotificationSeverity {
	Error = 30,
	Fatal = 40,
	Info = 10,
	Success = 0,
	Warn = 20,
}

export enum NotificationReadState {
	Read = 0,
	UnRead = 1,
}

interface NotificationData {
	extraProperties: { [key: string]: any };
	type: string;
}

interface UserIdentifier {
	userId: string;
	userName?: string;
}

interface NotificationSendInput {
	/** Current culture (required for template rendering) */
	culture?: string;
	/** Notification data */
	data: Dictionary<string, any>;
	/** Notification (template name for template notification) name */
	name: string;
	/** Urgency */
	severity?: NotificationSeverity;
	/** Recipient list */
	toUsers?: UserIdentifier[];
}

type NotificationTemplateSendInput = NotificationSendInput;

interface NotificationInfo {
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
	/** Type */
	type: NotificationType;
}

interface Notification {
	[key: string]: any;
	contentType: NotificationContentType;
	creationTime: Date;
	data: Record<string, any>;
	description?: string;
	lifetime: NotificationLifetime;
	message: string;
	name: string;
	severity: NotificationSeverity;
	title: string;
	type: NotificationType;
}

export type { Notification, NotificationData, NotificationInfo, NotificationSendInput, NotificationTemplateSendInput };
