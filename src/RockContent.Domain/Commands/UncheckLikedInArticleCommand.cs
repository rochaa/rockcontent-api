using System;
using Flunt.Validations;
using RockContent.Shared.Commands;

namespace RockContent.Domain.Commands
{
    public class UncheckLikedInArticleCommand : Command
    {
        public string User { get; set; }
        public Guid ArticleId { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract<UncheckLikedInArticleCommand>()
                .IsNotNullOrEmpty(User, nameof(User))
                .IsTrue(ArticleId != Guid.Empty, nameof(ArticleId))
            );
        }
    }
}