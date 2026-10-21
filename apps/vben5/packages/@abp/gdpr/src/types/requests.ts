import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/core';

interface GdprRequestDto extends EntityDto<string> {
  /** Creation time */
  creationTime: string;
  /** Ready time */
  readyTime: string;
}

interface GdprRequestGetListInput extends PagedAndSortedResultRequestDto {
  /** Creation time */
  creationTime?: string;
  /** Ready time */
  readyTime?: string;
}

export type { GdprRequestDto, GdprRequestGetListInput };
