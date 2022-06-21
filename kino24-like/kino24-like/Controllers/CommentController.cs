using kino24_like.BL.Interfaces;
using kino24_like.BL.Interfaces.Comment;
using kino24_like.BL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kino24_like.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IUserTokenService _userTokenService;

        public CommentController(
            ICommentService commentService,
            IUserTokenService userTokenService)
        {
            _commentService = commentService;
            _userTokenService = userTokenService;
        }

        [HttpGet("health")]
        [AllowAnonymous]
        public async Task<IActionResult> HealthCheck()
        {
            return Ok();
        }

        /// <summary>
        /// Method for adding comment for article
        /// </summary>
        /// <param name="itemId">ArticleId to add comment</param>
        /// <param name="text">Comment text</param>
        /// <returns>Result of adding comment</returns>
        /// <returns>Answer from backend for adding comment method</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Item not found</response>
        /// <response code="400">Problems with adding comment</response>
        [HttpPost("add")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddCommentAsync([FromBody] CommentArticleAddComand comand)
        { 
            var _event =new CommentArticleAdd () { ArticleId = comand.ArticleId, Text = comand.Text};
            if (!Guid.TryParse(comand.ArticleId, out _))
                return BadRequest("Invalid Id");
            try
            {
                await _commentService.AddCommentAsync(_event, _userTokenService.GetUserFromToken(await HttpContext.GetTokenAsync("access_token")));
            }
            catch (NullReferenceException)
            {
                return NotFound("Article not found");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { commentId = _event.CommentId });
        }

        /// <summary>
        /// Method for removing comment for article
        /// </summary>
        /// <param name="commentId">CommentId to remove</param>
        /// <returns>Result of removing comment</returns>
        /// <returns>Answer from backend for removing comment method</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Item not found</response>
        /// <response code="400">Problems with removing comment</response>
        [HttpPost("remove")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> RemoveCommentAsync([FromBody] CommentArticleRemoveComand comand)
        {
            if (!Guid.TryParse(comand.CommentId, out _))
                return BadRequest("Invalid Id");
            try
            {
                await _commentService.RemoveCommentAsync(new CommentArticleRemove { CommentId = comand.CommentId }, _userTokenService.GetUserFromToken(await HttpContext.GetTokenAsync("access_token")));
            }
            catch (NullReferenceException)
            {
                return NotFound("Comment not found or access denied");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Comment removed");
        }
    }
}
