using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DragonBlog2.Models
{
    public class Post
    {
        // Keys
        public int Id { get; set; }
        public int BlogId { get; set; }
 

        //Post Properties
        public string Title { get; set; }

        public string Abstract { get; set; }

        public string Content { get; set; }


        [Display(Name = "File Name")]
        public string FileName { get; set; }
        public byte[] Image { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }
        public bool IsPublished { get; set; }

        // Navigation
        public virtual Blog Blog { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public Post()
        {
            Comments = new HashSet<Comment>();
            Tags = new HashSet<Tag>();
        }
    }
}
