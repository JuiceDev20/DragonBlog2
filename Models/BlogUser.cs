using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DragonBlog2.Models
{
    public class BlogUser : IdentityUser
    {
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }
        public string DisplayName { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public BlogUser()
        {
            Comments = new HashSet<Comment>();
            DisplayName = "New User";

        }
    }
} 
