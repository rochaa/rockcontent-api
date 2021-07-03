using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RockContent.Domain.Dtos;
using RockContent.Domain.Entities;
using RockContent.Shared.Data;

namespace RockContent.Domain.Repositories
{
    public interface IArticleRepository : IRepository<Article>
    {
        Task<Article> GetById(Guid id);
        Task<IEnumerable<ArticleToShowDto>> GetAll(string user);

        Task<bool> HasLikeToUser(Guid articleId, string user);
        Task<Like> GetLikeByUser(Guid articleId, string user);
        void Insert(Like like);
        void Delete(Like like);
    }
}