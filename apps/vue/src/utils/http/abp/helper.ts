/**
 * Format paging requests suitable for abp framework
 * The definition of paging by the abp framework is as follows:
 * skipCount: From which offset position to request the entity list
 * maxResultCount: Returns the number of entity lists
 */
export function formatPagedRequest(request: { skipCount: number; maxResultCount: number }) {
  if (request.skipCount > 0) {
    request.skipCount -= 1;
  }
  request.skipCount = request.skipCount * request.maxResultCount;
}
