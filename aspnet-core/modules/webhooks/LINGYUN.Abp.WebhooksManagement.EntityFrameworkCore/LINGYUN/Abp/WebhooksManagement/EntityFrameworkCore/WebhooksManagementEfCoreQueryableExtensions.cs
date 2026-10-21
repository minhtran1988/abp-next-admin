using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LINGYUN.Abp.WebhooksManagement.EntityFrameworkCore;

public static class WebhooksManagementEfCoreQueryableExtensions
{
    // Extension method of aggregating warehousing services here
    public static IQueryable<WebhookSendRecord> IncludeDetails(this IQueryable<WebhookSendRecord> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.WebhookEvent);
    }
}
