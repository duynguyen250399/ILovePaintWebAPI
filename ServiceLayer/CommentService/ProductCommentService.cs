using DataLayer.Data;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer.CommentService
{
    public class ProductCommentService : IProductCommentService
    {
        private readonly AppDbContext _context;

        public ProductCommentService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductComment> GetAllComments(int productID)
        {
            return _context.ProductComments.Where(c => c.ProductID == productID)
                                            .Include(c => c.User)
                                            .Include(c => c.CommentReplies)
                                                .ThenInclude(r => r.User);
        }

        public async Task<ProductComment> PostCommentAsync(ProductComment comment)
        {
            await _context.ProductComments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<CommentReply> PostCommentReplyAsync(CommentReply reply)
        {
            await _context.CommentReplies.AddAsync(reply);
            await _context.SaveChangesAsync();

            return reply;
        }
    }
}
