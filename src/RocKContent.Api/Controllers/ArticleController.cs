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
            // TODO: To increase resiliency, this command can be sent to a messaging service (rabbitMQ, Kafka, Azure Service Bus)
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
            // TODO: To increase resiliency, this command can be sent to a messaging service (RabbitMQ, Kafka, Azure Service Bus)
            var result = await _mediator.SendCommand(command);

            if (!result.Sucess)
                return BadRequest(result);

            return Ok(result);
        }

        [Route("all/{user}")]
        [HttpGet]
        public async Task<ActionResult> QuestionGetAll([FromRoute] string user)
        {
            // TODO: Caching can be implemented here. If the application has millions of hits in a short period. (Cache Asp.net or Redis)
            var result = await _articleRepository.GetAll(user);
            return Ok(result);
        }
    }
}