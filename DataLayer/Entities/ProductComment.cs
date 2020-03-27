using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Entities
{
    public class ProductComment
    {
        public int ID { get; set; }
        [Required]
        public string UserID { get; set; }
        public User User { get; set; }
        public string Content { get; set; }      
        public DateTime CommentDate { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public string Role { get; set; }
        public IEnumerable<CommentReply> CommentReplies { get; set; }

    }
}
