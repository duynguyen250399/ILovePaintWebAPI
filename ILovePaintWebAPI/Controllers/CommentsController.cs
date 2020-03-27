using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.CommentService;

namespace ILovePaintWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IProductCommentService _commentService;

        public CommentsController(IProductCommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        [Route("{productId}")]
        public IActionResult GetComments(int productId)
        {
            var comments = _commentService.GetAllComments(productId);

            if(comments == null)
            {
                return NotFound(new { message = "Comments not found!" });
            }

            return Ok(comments);
        }

        public async Task<IActionResult> PostComment(ProductComment comment)
        {
            if(comment == null)
            {
                return BadRequest(new { message = "Comment is null!" });
            }

            if(string.IsNullOrEmpty(comment.Content))
            {
                return BadRequest(new { message = "Missing comment content" });
            }

            if (string.IsNullOrEmpty(comment.Role))
            {
                return Unauthorized(new { message = "User is unauthorized!" });
            }

            var newComment = await _commentService.PostCommentAsync(comment);

            return Ok(new
            {
                status = "success",
                message = "Comment posted"
            });
        }

        [HttpPost]
        [Route("reply")]
        public async Task<IActionResult> PostCommentReply(CommentReply reply)
        {
            if(reply == null)
            {
                return BadRequest(new { message = "Reply is null!" });
            }

            if (string.IsNullOrEmpty(reply.Content))
            {
                return BadRequest(new { message = "Missing comment content" });
            }

            if (string.IsNullOrEmpty(reply.Role))
            {
                return Unauthorized(new { message = "User is unauthorized!" });
            }

            var newReply = await _commentService.PostCommentReplyAsync(reply);

            return Ok(new
            {
                status = "success",
                message = "Reply posted"
            });
        }
    }
}