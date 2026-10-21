using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace PackageName.CompanyName.ProjectName.Users
{
    /// <summary>
    /// User entity
    /// </summary>
    public class User : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// Username
        /// </summary>
        [MaxLength(50)]
        public string NickName { get; set; }

        /// <summary>
        /// IdentityUserId
        /// </summary>
        public Guid IdentityUserId { get; set; }

        /// <summary>
        /// Identity user
        /// </summary>
        public virtual IdentityUser IdentityUser { get; set; }
        
        /// <summary>
        /// Contact information
        /// </summary>
        [MaxLength(50)]
        public string ContactInfo { get; set; }
        
        /// <summary>
        /// Position
        /// </summary>
        [MaxLength(50)]
        public string Position { get; set; }

        protected User()
        {
        }

        public User(
            Guid id,
            string nickName,
            Guid identityUserId,
            string contactInfo = null,
            string position = null
        ) : base(id)
        {
            NickName = nickName;
            IdentityUserId = identityUserId;
            ContactInfo = contactInfo;
            Position = position;
        }
    }
}
