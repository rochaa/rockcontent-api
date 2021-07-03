using System.Threading;
using System.Threading.Tasks;
using RockContent.Domain.Entities;
using RockContent.Domain.Repositories;
using RockContent.Shared.Commands;

namespace RockContent.Domain.Commands
{
    public class ArticleLikeCommandHandler :
        ICommandHandler<MarkLikedInArticleCommand>,
        ICommandHandler<UncheckLikedInArticleCommand>
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleLikeCommandHandler(
            IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<CommandResult> Handle(MarkLikedInArticleCommand command, CancellationToken ct = default)
        {
            command.Validate();
            if (!command.IsValid)
                return new CommandResult("Input data invalid", command.Notifications);

            var article = await _articleRepository.GetById(command.ArticleId);
            if (article == null)
                return new CommandResult("Article not found");

            var alreadyLike = await _articleRepository.HasLikeToUser(article.Id, command.User);
            if (alreadyLike)
                return new CommandResult("User already mark liked in the article");

            var like = new Like(command.User, command.ArticleId);
            _articleRepository.Insert(like);
            await _articleRepository.UnitOfWork.Commit();

            return new CommandResult("Like successfully registered", new object { });
        }

        public async Task<CommandResult> Handle(UncheckLikedInArticleCommand command, CancellationToken ct = default)
        {
            command.Validate();
            if (!command.IsValid)
                return new CommandResult("Input data invalid", command.Notifications);

            var article = await _articleRepository.GetById(command.ArticleId);
            if (article == null)
                return new CommandResult("Article not found");

            var like = await _articleRepository.GetLikeByUser(article.Id, command.User);
            if (like == null)
                return new CommandResult("User not mark liked in the article");

            _articleRepository.Delete(like);
            await _articleRepository.UnitOfWork.Commit();

            return new CommandResult("Dislike successfully registered", new object { });
        }
    }
}