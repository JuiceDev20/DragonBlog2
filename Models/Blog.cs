using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonBlog2.Models
{
    public class Blog
    {
        //This postis intended for categorization of posts

        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        //Navigation
        public virtual ICollection<Post> Posts { get; set; }

        public Blog()
        {
            Posts = new HashSet<Post>();
        }
        
        

       
    }
}
