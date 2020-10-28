using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonBlog2.Models
{
    public class Comment
    {
        // Keys
        public int Id { get; set; }
        public int PostId { get; set; }
        public string BlogUserId { get; set; }


        //Comment Properties
        public string Content { get; set; }
        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        //Navigation
        public virtual Post Post { get; set; }

        public virtual BlogUser BlogUser { get; set; }

    }
}
