/* eslint-disable @typescript-eslint/no-non-null-assertion */
import type { HubConnection, IHttpConnectionOptions } from "@microsoft/signalr";

import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { useEventBus } from "../useEventBus";
import useUserStore from "@/store/userStore";

interface SignalROptions {
	/** Automatic reconnection after disconnection */
	automaticReconnect?: boolean;
	/** Initialize and automatically establish a connection */
	autoStart?: boolean;
	/** Next retry interval (ms) */
	nextRetryDelayInMilliseconds?: number;
	/** Server url */
	serverUrl: string;
	/** Whether to carry access token */
	useAccessToken?: boolean;
}

export function useSignalR() {
	const { publish, subscribe } = useEventBus();

	let connection: HubConnection | null = null;

	/** Initialize SignalR */
	async function init({
		automaticReconnect = true,
		autoStart = false,
		nextRetryDelayInMilliseconds = 60_000,
		serverUrl,
		useAccessToken = true,
	}: SignalROptions) {
		const httpOptions: IHttpConnectionOptions = {};
		if (useAccessToken) {
			httpOptions.accessTokenFactory = () => {
				const { userToken } = useUserStore.getState();
				const token = userToken.accessToken;
				if (!token) {
					return "";
				}
				return token.startsWith("Bearer ") ? token.slice(7) : token;
			};
		}
		const connectionBuilder = new HubConnectionBuilder()
			.withUrl(serverUrl, httpOptions)
			.configureLogging(LogLevel.Warning);
		if (automaticReconnect && nextRetryDelayInMilliseconds) {
			connectionBuilder.withAutomaticReconnect({
				nextRetryDelayInMilliseconds: () => nextRetryDelayInMilliseconds,
			});
		}
		connection = connectionBuilder.build();
		if (autoStart) {
			await start();
		}
	}

	/** Start connection */
	async function start(): Promise<void> {
		_throwIfNotInit();
		publish("signalR:beforeStart");
		try {
			await connection?.start();
			publish("signalR:onStart");
		} catch (error) {
			publish("signalR:onError", error);
		}
	}

	/** Close connection */
	async function stop(): Promise<void> {
		_throwIfNotInit();
		publish("signalR:beforeStop");
		try {
			await connection?.stop();
			publish("signalR:onStop");
		} catch (error) {
			publish("signalR:onError", error);
		}
	}

	/** Pre-connection events */
	function beforeStart<T = any>(callback: (event?: T) => void) {
		subscribe("signalR:beforeStart", callback);
	}

	/** Post-connection events */
	function onStart<T = any>(callback: (event?: T) => void) {
		subscribe("signalR:onStart", callback);
	}

	/** Event before closing connection */
	function beforeStop<T = any>(callback: (event?: T) => void) {
		subscribe("signalR:beforeStop", callback);
	}

	/** Event after closing the connection */
	function onStop<T = any>(callback: (event?: T) => void) {
		subscribe("signalR:onStop", callback);
	}

	/** Connection error event */
	function onError(callback: (error?: Error) => void) {
		subscribe("signalR:onError", callback);
	}

	/** Subscribe to server messages */
	function on(methodName: string, newMethod: (...args: any[]) => void): void {
		connection?.on(methodName, newMethod);
	}

	/** Log out server message */
	function off(methodName: string, method: (...args: any[]) => void): void {
		connection?.off(methodName, method);
	}

	/** Connection close event */
	function onClose(callback: (error?: Error) => void): void {
		connection?.onclose(callback);
	}

	/** Send message */
	function send(methodName: string, ...args: any[]): Promise<void> {
		_throwIfNotInit();
		if (!connection) {
			return Promise.reject(new Error("Connection is not initialized"));
		}
		return connection.send(methodName, ...args);
	}

	/** Call function */
	function invoke<T = any>(methodName: string, ...args: any[]): Promise<T> {
		_throwIfNotInit();
		if (connection) {
			return connection.invoke(methodName, ...args);
		}
		return Promise.reject(new Error("Connection is not initialized"));
	}

	function _throwIfNotInit() {
		if (connection === null) {
			throw new Error("unable to send message, connection not initialized!");
		}
	}

	return {
		beforeStart,
		beforeStop,
		init,
		invoke,
		off,
		on,
		onClose,
		onError,
		onStart,
		onStop,
		send,
		start,
		stop,
	};
}
