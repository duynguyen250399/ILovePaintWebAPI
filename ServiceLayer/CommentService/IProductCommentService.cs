using DataLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.CommentService
{
    public interface IProductCommentService
    {
        IEnumerable<ProductComment> GetAllComments(int productID);
        Task<ProductComment> PostCommentAsync(ProductComment comment);
        Task<CommentReply> PostCommentReplyAsync(CommentReply reply);
    }
}
