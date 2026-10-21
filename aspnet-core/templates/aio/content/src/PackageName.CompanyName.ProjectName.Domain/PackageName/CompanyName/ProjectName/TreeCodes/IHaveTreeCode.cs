using System;
using Volo.Abp.Auditing;

namespace PackageName.CompanyName.ProjectName.TreeCodes
{
    /// <summary>
    /// Define the entity interface with tree encoding
    /// </summary>
    public interface IHaveTreeCode : IHasCreationTime
    {
        /// <summary>
        /// Tree encoding
        /// </summary>
        string TreeCode { get; set; }
        
        /// <summary>
        /// ParentId
        /// </summary>
        Guid? ParentId { get; set; }
    }
}
