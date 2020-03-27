using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class CommentReply
    {
        public int ID { get; set; }
        public int ProductCommentID { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public string Content { get; set; }
        public DateTime ReplyDate { get; set; }
        public string Role { get; set; }
    }
}
