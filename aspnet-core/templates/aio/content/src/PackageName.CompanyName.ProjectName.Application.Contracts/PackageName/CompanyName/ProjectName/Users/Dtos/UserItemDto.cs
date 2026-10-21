using System;
using Volo.Abp.Application.Dtos;

namespace PackageName.CompanyName.ProjectName.Users.Dtos
{
    [Serializable]
    public class UserItemDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// Username
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// IdentityUserId
        /// </summary>
        public Guid IdentityUserId { get; set; }

        /// <summary>
        /// User status
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Contact information
        /// </summary>
        public string ContactInfo { get; set; }
        
        /// <summary>
        /// Position
        /// </summary>
        public string Position { get; set; }
        
        /// <summary>
        /// role name
        /// </summary>
        public string RoleNames { get; set; }
    }
}