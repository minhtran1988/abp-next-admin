interface ComponentKey {
  /** component name */
  name: string;
  /** Component display name */
  displayName: string;
}

interface ComponentInfo {
  /** component name */
  name: string;
  /** Component name collection */
  keys: ComponentKey[];
  /** Component status collection */
  details: Record<string, any>;
}

interface SystemInfo {
  components: ComponentInfo[];
}

export type { SystemInfo };
