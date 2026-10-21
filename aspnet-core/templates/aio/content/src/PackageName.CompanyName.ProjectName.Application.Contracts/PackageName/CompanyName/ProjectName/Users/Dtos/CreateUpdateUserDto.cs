using System;
using System.ComponentModel.DataAnnotations;

namespace PackageName.CompanyName.ProjectName.Users.Dtos
{
    [Serializable]
    public class CreateUpdateUserDto
    {
        /// <summary>
        /// Username
        /// </summary>
        [Required(ErrorMessage = "User name cannot be empty")]
        [StringLength(50, ErrorMessage = "User name cannot exceed 50 characters in length")]
        public string NickName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password length must be between 6-20 characters")]
        public string Password { get; set; }

        /// <summary>
        /// Contact information
        /// </summary>
        public string ContactInfo { get; set; }

        /// <summary>
        /// Position
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Whether to enable
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}