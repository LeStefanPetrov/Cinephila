﻿using System.ComponentModel.DataAnnotations;

namespace Cinephila.DataAccess.Entities
{
    public class UserEntity : BaseEntity
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Picture { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
