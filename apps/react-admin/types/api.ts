export interface Result<T = any> {
	code: string;
	/** Error details */
	details?: string;
	message: string;
	result: T;
}
