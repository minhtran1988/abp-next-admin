using LINGYUN.Abp.Dynamic.Queryable;
using PackageName.CompanyName.ProjectName.Localization;

namespace PackageName.CompanyName.ProjectName;
/// <summary>
/// Provide dynamic query controller implementation
/// </summary>
/// <typeparam name="TEntityDto">Entity dto type</typeparam>
public abstract class ProjectNameDynamicQueryableControllerBase<TEntityDto> : DynamicQueryableControllerBase<TEntityDto>
{
    protected ProjectNameDynamicQueryableControllerBase(
        IDynamicQueryableAppService<TEntityDto> service)
        : base(service)
    {
        LocalizationResource = typeof(ProjectNameResource);
    }
}
