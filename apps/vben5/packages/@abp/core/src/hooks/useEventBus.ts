import type { EventType, Handler, WildcardHandler } from '../utils/mitt';

import mitt from '../utils/mitt';

const emitter = mitt();

interface EventBus {
  /** Publish event */
  publish(type: '*', event?: any): void;
  /** Publish event */
  publish<T = any>(type: EventType, event?: T): void;

  /** Subscribe to events */
  subscribe(type: '*', handler: WildcardHandler): void;
  /** Subscribe to events */
  subscribe<T = any>(type: EventType, handler: Handler<T>): void;

  /** Unsubscribe event */
  unSubscribe(type: '*', handler: WildcardHandler): void;
  /** Unsubscribe event */
  unSubscribe<T = any>(type: EventType, handler: Handler<T>): void;
}

export function useEventBus(): EventBus {
  return {
    publish: emitter.emit,
    subscribe: emitter.on,
    unSubscribe: emitter.off,
  };
}
