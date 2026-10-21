import type { ListResultDto } from '@abp/core';

import type {
  IdentityClaimCreateDto,
  IdentityClaimDeleteDto,
  IdentityClaimDto,
  IdentityClaimUpdateDto,
} from '../../types/claims';

interface ClaimEditModalProps {
  /** Add new declaration api */
  createApi: (input: IdentityClaimCreateDto) => Promise<void>;
  /** Add new declaration permission code */
  createPolicy: string;
  /** Delete declaration api */
  deleteApi: (input: IdentityClaimDeleteDto) => Promise<void>;
  /** Delete declaration permission code */
  deletePolicy: string;
  /** Update statement api */
  updateApi: (input: IdentityClaimUpdateDto) => Promise<void>;
  /** Update statement permission code */
  updatePolicy: string;
}

interface ClaimModalProps extends ClaimEditModalProps {
  /** Load declaration list api */
  getApi: () => Promise<ListResultDto<IdentityClaimDto>>;
}

interface ClaimModalEvents {
  (event: 'change', data: IdentityClaimDto): void;
  (event: 'onDelete'): void;
}

export type { ClaimEditModalProps, ClaimModalEvents, ClaimModalProps };
