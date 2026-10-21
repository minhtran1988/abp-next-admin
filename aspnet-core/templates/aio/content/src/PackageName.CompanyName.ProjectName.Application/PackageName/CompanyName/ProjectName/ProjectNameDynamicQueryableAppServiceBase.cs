using LINGYUN.Abp.Dynamic.Queryable;
using PackageName.CompanyName.ProjectName.Localization;

namespace PackageName.CompanyName.ProjectName;
/// <summary>
/// Provide dynamic query interface implementation
/// </summary>
/// <typeparam name="TEntity">Entity type</typeparam>
/// <typeparam name="TEntityDto">Entity dto type</typeparam>
public abstract class ProjectNameDynamicQueryableAppServiceBase<TEntity, TEntityDto> :
    DynamicQueryableAppService<TEntity, TEntityDto>,
    IProjectNameDynamicQueryableAppService<TEntityDto>
{
    protected ProjectNameDynamicQueryableAppServiceBase()
    {
        LocalizationResource = typeof(ProjectNameResource);
        ObjectMapperContext = typeof(ProjectNameApplicationModule);
    }
}
