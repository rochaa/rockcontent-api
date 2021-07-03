using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RockContent.Domain.Commands;
using RockContent.Domain.Repositories;
using RockContent.Shared.Mediator;

namespace RocKContent.Api.Controllers
{
    [ApiController]
    [Route("v1/article")]
    [Authorize]
    public class ArticleController : ControllerBase
    {
        private readonly IMediatorHandler _mediator;

        public readonly IArticleRepository _articleRepository;

        public ArticleController(IMediatorHandler mediator, IArticleRepository articleRepository)
        {
            _mediator = mediator;
            _articleRepository = articleRepository; 
        }

        [Route("like")]
        [HttpPost]
        public async Task<ActionResult> ArticleLikePost(
            [FromBody] MarkLikedInArticleCommand command)
        {
            var result = await _mediator.SendCommand(command);

            if (!result.Sucess)
                return BadRequest(result);

            return Ok(result);
        }

        [Route("dislike")]
        [HttpPost]
        public async Task<ActionResult> ArticleDislikePost(
            [FromBody] UncheckLikedInArticleCommand command)
        {
            var result = await _mediator.SendCommand(command);

            if (!result.Sucess)
                return BadRequest(result);

            return Ok(result);
        }

        [Route("all")]
        [HttpGet]
        public async Task<ActionResult> QuestionGetAll()
        {
            var result = await _articleRepository.GetAll();
            return Ok(result);
        }
    }
}