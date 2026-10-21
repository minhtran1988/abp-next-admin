import type { ComputedRef, MaybeRef } from 'vue';

/**
 * Increase depth count in type-level recursion
 */
type Increment<A extends unknown[]> = [...A, unknown];
/**
 * All properties of deep recursion are optional
 */
type DeepPartial<
  T,
  D extends number = 10,
  C extends unknown[] = [],
> = C['length'] extends D
  ? T
  : T extends object
    ? {
        [P in keyof T]?: DeepPartial<T[P], D, Increment<C>>;
      }
    : T;

/**
 * All properties of deep recursion are read-only
 */
type DeepReadonly<
  T,
  D extends number = 10,
  C extends unknown[] = [],
> = C['length'] extends D
  ? T
  : T extends object
    ? {
        readonly [P in keyof T]: DeepReadonly<T[P], D, Increment<C>>;
      }
    : T;

/**
 * Any type of asynchronous function
 */

type AnyPromiseFunction<T extends any[] = any[], R = void> = (
  ...arg: T
) => PromiseLike<R>;

/**
 * Ordinary functions of any type
 */
type AnyNormalFunction<T extends any[] = any[], R = void> = (...arg: T) => R;

/**
 * Any type of function
 */
type AnyFunction<T extends any[] = any[], R = void> =
  | AnyNormalFunction<T, R>
  | AnyPromiseFunction<T, R>;

/**
 * T | null packaging
 */
type Nullable<T> = null | T;

/**
 * T | Not null packaging
 */
type NonNullable<T> = T extends null | undefined ? never : T;

/**
 * String type object
 */
type Recordable<T> = Record<string, T>;

/**
 * String type object (read-only)
 */
interface ReadonlyRecordable<T = any> {
  readonly [key: string]: T;
}

/**
 * setTimeout return value type
 */
type TimeoutHandle = ReturnType<typeof setTimeout>;

/**
 * setInterval return value type
 */
type IntervalHandle = ReturnType<typeof setInterval>;

/**
 * Maybe it's a computed ref, or a getter function
 *
 */
type MaybeReadonlyRef<T> = (() => T) | ComputedRef<T>;

/**
 * Maybe it's a ref, or a normal value, or a getter function
 *
 */
type MaybeComputedRef<T> = MaybeReadonlyRef<T> | MaybeRef<T>;

type Merge<O extends object, T extends object> = {
  [K in keyof O | keyof T]: K extends keyof T
    ? T[K]
    : K extends keyof O
      ? O[K]
      : never;
};

/**
 * T = [
 *  { name: string; age: number; },
 *  { sex: 'male' | 'female'; age: string }
 * ]
 * =>
 * MergeAll<T> = {
 *  name: string;
 *  sex: 'male' | 'female';
 *  age: string
 * }
 */
type MergeAll<
  T extends object[],
  R extends object = Record<string, any>,
> = T extends [infer F extends object, ...infer Rest extends object[]]
  ? MergeAll<Rest, Merge<R, F>>
  : R;

type EmitType = (name: Name, ...args: any[]) => void;

type MaybePromise<T> = Promise<T> | T;

export type {
  AnyFunction,
  AnyNormalFunction,
  AnyPromiseFunction,
  DeepPartial,
  DeepReadonly,
  EmitType,
  IntervalHandle,
  MaybeComputedRef,
  MaybePromise,
  MaybeReadonlyRef,
  Merge,
  MergeAll,
  NonNullable,
  Nullable,
  ReadonlyRecordable,
  Recordable,
  TimeoutHandle,
};
