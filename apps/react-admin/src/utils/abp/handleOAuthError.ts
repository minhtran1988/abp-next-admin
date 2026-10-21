interface OAuthError {
	error: string;
	error_description?: string;
	error_uri?: string;
}

export function handleOAuthError() {
	function formatError(error: OAuthError) {
		// TODO: Solve the internationalization of oauth messages.
		return error.error_description;
	}

	return {
		formatError,
	};
}
