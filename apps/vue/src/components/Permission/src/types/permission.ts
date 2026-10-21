export interface PermissionProps {
  providerName: string;
  providerKey?: string;
  readonly: boolean;
  identity?: string;
}

export interface PermissionTree {
  isRoot: boolean;
  /** Permission identification */
  name: string;
  /** display name */
  displayName: string;
  /** Authorization or not */
  isGranted?: boolean;
  /** Whether to disable */
  disabled: boolean;
  /** Child node */
  children: PermissionTree[];
  /** Parent node */
  parentName?: string;
}
