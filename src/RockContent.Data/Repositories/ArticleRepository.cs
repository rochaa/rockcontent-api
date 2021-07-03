using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RockContent.Domain.Entities;
using RockContent.Domain.Repositories;
using RockContent.Shared.Data;

namespace RockContent.Data.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly RockContentContext _context;

        public ArticleRepository(RockContentContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Article> GetById(Guid id)
        {
            return await _context.Articles.FindAsync(id);
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            return await _context.Articles.AsNoTrackingWithIdentityResolution().ToListAsync();
        }

        public async Task<Like> GetLikeByUser(Guid articleId, string user)
        {
            return await _context.Likes
                .FirstOrDefaultAsync(l => l.ArticleId == articleId &&
                                          l.User == user);
        }

        public async Task<bool> HasLikeToUser(Guid articleId, string user)
        {
            return await _context.Likes
                .AnyAsync(a => a.ArticleId == articleId &&
                          a.User == user);
        }

        public void Insert(Like like)
        {
            _context.Likes.Add(like);
        }

        public void Delete(Like like)
        {
            _context.Likes.Remove(like);
        }

        public void Dispose() => _context?.Dispose();
    }
}