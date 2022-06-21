using kino24_like.BL.Interfaces;
using kino24_like.BL.Interfaces.Comment;
using kino24_like.BL.Interfaces.Like;
using kino24_like.BL.Models;
using kino24_like.BL.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kino24_like.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeArticleController : ControllerBase
    {
        private readonly ILikeArticleService _likeService;
        private readonly IUserTokenService _userTokenService;


        public LikeArticleController(
            ILikeArticleService likeService,
            IUserTokenService userTokenService)
        {
            _likeService  = likeService;
            _userTokenService = userTokenService;
        }

        /// <summary>
        /// Method for adding like for article
        /// </summary>
        /// <param name="articleId">ArticleId to add like</param>
        /// <returns>Result of adding like</returns>
        /// <returns>Answer from backend for adding like method</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Item not found</response>
        /// <response code="400">Problems with adding like</response>
        [HttpPost("add")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddLikeAsync([FromBody] LikeArticleAddComand comand)
        {
            if (!Guid.TryParse(comand.ArticleId, out _))
                return BadRequest("Invalid Id");
            try
            {
                await _likeService.AddLikeAsync(new LikeArticleAdd { ArticleId = comand.ArticleId }, _userTokenService.GetUserFromToken(await HttpContext.GetTokenAsync("access_token")));
            }
            catch (NullReferenceException)
            {
                return NotFound("Article not found");
            }
            catch(InvalidOperationException)
            {
                return BadRequest("Article is already liked");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Article liked");
        }

        /// <summary>
        /// Method for removing like for article
        /// </summary>
        /// <param name="articleId">ArticleId to remove like</param>
        /// <returns>Result of removing like</returns>
        /// <returns>Answer from backend for removing like method</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Item not found</response>
        /// <response code="400">Problems with adding like</response>
        [HttpPost("remove")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> RemoveLikeAsync([FromBody] LikeArticleRemoveComand comand)
        {
            if (!Guid.TryParse(comand.ArticleId, out _))
                return BadRequest("Invalid Id");
            try
            {
                await _likeService.RemoveLikeAsync(new LikeArticleRemove { ArticleId = comand.ArticleId }, _userTokenService.GetUserFromToken(await HttpContext.GetTokenAsync("access_token")));
            }
            catch (NullReferenceException)
            {
                return NotFound("Like is already removed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Like removed");
        }

        /// <summary>
        /// Method for adding dislike for article
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns>Result of adding dislike</returns>
        /// <returns>Answer from backend for adding dislike method</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Item not found</response>
        /// <response code="400">Problems with adding dislike</response>
        //[HttpPost("addDislike")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        //public async Task<IActionResult> AddDisLikeAsync([FromBody] string articleId)
        //{
        //    try
        //    {
        //        await _likeService.AddDislikeAsync(new DislikeArticleAdd { ArticleId = articleId }, _userTokenService.GetUserFromToken(await HttpContext.GetTokenAsync("access_token")));
        //    }
        //    catch (NullReferenceException)
        //    {
        //        return NotFound("Article not found");
        //    }
        //    catch (InvalidOperationException)
        //    {
        //        return BadRequest("Article is already liked");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    return Ok("Article liked");
        //}

        /// <summary>
        /// Method for removing like for article
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns>Result of removing like</returns>
        /// <returns>Answer from backend for removing like method</returns>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Item not found</response>
        /// <response code="400">Problems with adding like</response>
        //[HttpDelete("removeDislike")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        //public async Task<IActionResult> RemoveDisLikeAsync([FromBody] string articleId)
        //{
        //    try
        //    {
        //        await _likeService.RemoveDislikeAsync(new DislikeArticleRemove { ArticleId = articleId }, _userTokenService.GetUserFromToken(await HttpContext.GetTokenAsync("access_token")));
        //    }
        //    catch (NullReferenceException)
        //    {
        //        return NotFound("Like is already removed");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    return Ok("Like removed");
        //}
    }
}
