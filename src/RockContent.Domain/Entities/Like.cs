using System;
using RockContent.Shared.Entities;

namespace RockContent.Domain.Entities
{
    public class Like : Entity
    {
        protected Like() { }

        public Like(string user, Guid articleId)
        {
            User = user;
            ArticleId = articleId;
            DateLike = DateTime.Now;
        }

        public string User { get; private set; }
        public Guid ArticleId { get; private set; }
        public DateTime DateLike { get; private set; }

        public Article Article { get; private set; }
    }
}