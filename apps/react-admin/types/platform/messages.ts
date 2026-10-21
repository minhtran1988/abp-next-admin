import type { AuditedEntityDto, PagedAndSortedResultRequestDto } from "#/abp-core";

/** Message status */
export enum MessageStatus {
	/** Sending failed */
	Failed = 10,
	/** Not sent */
	Pending = -1,
	/** Sent */
	Sent = 0,
}
/** Email priority */
export enum MailPriority {
	/** high */
	High = 2,
	/** low */
	Low = 1,
	/** Normal */
	Normal = 0,
}

interface MessageDto extends AuditedEntityDto<string> {
	/** Message content */
	content: string;
	/** Message publisher */
	provider?: string;
	/** Error reason */
	reason?: string;
	/** Receiver */
	receiver: string;
	/** Number of times sent */
	sendCount: number;
	/** Sender */
	sender?: string;
	/** Send time */
	sendTime?: Date;
	/** status */
	status: MessageStatus;
	/** Sender Id */
	userId?: string;
}
/** Email attachment */
interface EmailMessageAttachmentDto {
	/** Attachment storage name */
	blobName: string;
	/** Attachment name */
	name: string;
	/** Attachment size */
	size: number;
}
/** Email headers */
interface EmailMessageHeaderDto {
	/** Key name */
	key: string;
	/** key value */
	value: string;
}
/** Email message */
interface EmailMessageDto extends MessageDto {
	attachments: EmailMessageAttachmentDto[];
	cc?: string;
	from?: string;
	headers: EmailMessageHeaderDto[];
	isBodyHtml: boolean;
	normalize: boolean;
	priority?: MailPriority;
	subject?: string;
}

interface EmailMessageGetListInput extends PagedAndSortedResultRequestDto {
	beginSendTime?: Date;
	content?: string;
	emailAddress?: string;
	endSendTime?: Date;
	from?: string;
	priority?: MailPriority;
	subject?: string;
}

type SmsMessageDto = MessageDto;

interface SmsMessageGetListInput extends PagedAndSortedResultRequestDto {
	beginSendTime?: Date;
	content?: string;
	endSendTime?: Date;
	phoneNumber?: string;
}

export type { EmailMessageDto, EmailMessageGetListInput, SmsMessageDto, SmsMessageGetListInput };
