export interface BasicPageParams {
    page: number;
    pageSize: number;
  }
  
  export interface BasicFetchResult<T> {
    items: T[];
    total: number;
  }
  
  export interface IAvailable {
    isAvailable: boolean;
  }
  
  export class Available implements IAvailable {
    isAvailable!: boolean;
  }
  
  /** Authorization interface */
  export interface IPermission {
    /** Permission name */
    name: string;
    /** Authorization or not */
    isGranted: boolean;
  }
  
  /** Acts on the interface */
  export interface IScope {
    /** Scope */
    scope: string;
  }
  
  /** Key */
  export interface ISecret {
    /** Key type */
    type: string;
    /** Key value */
    value: string;
    /** Key description */
    description: string | undefined;
    /** Expiration date */
    expiration: Date | undefined;
  }
  
  /** Token */
  export interface IClaim {
    /** Type */
    type: string;
    /** Value */
    value: string;
  }
  
  export class SecretBase implements ISecret {
    type = '';
    value = '';
    description = '';
    expiration: Date | undefined;
  }
  
  export enum HashType {
    Sha256,
    Sha512,
  }
  
  export class Claim implements IClaim {
    type = '';
    value = '';
  }
  
  export class KeyValue<TKey, TValue> {
    key!: TKey;
    value?: TValue;
  }
  