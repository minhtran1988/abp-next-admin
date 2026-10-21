/** Data access policy */
enum DataAccessStrategy {
  /** All data can be accessed */
  All = 0,
  /** Only the current user organization and subordinate organizations */
  CurrentAndSubOrganizationUnits = 5,
  /** Only the current user organization */
  CurrentOrganizationUnits = 4,
  /** Current user role only */
  CurrentRoles = 3,
  /** Current user only */
  CurrentUser = 2,
  /** Custom rules */
  Custom = 1,
}

interface SubjectStrategyGetInput {
  subjectId: string;
  subjectName: string;
}

interface SubjectStrategySetInput {
  isEnabled: boolean;
  strategy: DataAccessStrategy;
  subjectId: string;
  subjectName: string;
}

interface SubjectStrategyDto {
  isEnabled: boolean;
  strategy: DataAccessStrategy;
  subjectId: string;
  subjectName: string;
}

export { DataAccessStrategy };

export type {
  SubjectStrategyDto,
  SubjectStrategyGetInput,
  SubjectStrategySetInput,
};
