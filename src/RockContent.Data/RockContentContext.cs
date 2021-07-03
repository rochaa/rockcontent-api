using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RockContent.Domain.Entities;
using RockContent.Shared.Data;

namespace RockContent.Data
{
    public class RockContentContext : DbContext, IUnitOfWork
    {
        public RockContentContext(DbContextOptions<RockContentContext> options) : base(options) { }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Generate articles to test.
            modelBuilder.Entity<Article>().HasData(new Article("Saiba como entrevistar um cliente", "Saber como entrevistar um cliente é uma maneira tradicional, porém poderosa, de garantir a competitividade da sua empresa e elevar a experiência dos consumidores com a sua marca. A estratégia serve para obter insights, tanto para valorizar as forças e as potencialidades do negócio quanto para implementar mudanças ou combater falhas e fraquezas.", "Anelise Margotti", DateTime.Now.AddDays(-2)));
            modelBuilder.Entity<Article>().HasData(new Article("Será que você sabe realmente o que é um bom ROI?", "Determinar o que é um bom ROI é importante para que uma empresa possa obter um resultado mais realista desse indicador, pois os valores mensurados variam conforme o setor de negócio ou o investimento realizado.", "Ivan de Souza", DateTime.Now.AddDays(-10)));
            modelBuilder.Entity<Article>().HasData(new Article("Confira as 7 principais técnicas para otimização de web sites", "A otimização de web sites é fundamental para que qualquer negócio fortaleça sua presença digital e consiga se destacar dentro de um cenário cada vez mais competitivo. Com as técnicas certas, suas páginas vão alcançar uma performance muito melhor e, consequentemente, os resultados do negócio como um todo vão melhorar.", "Gabriel Camargo", DateTime.Now.AddDays(-30)));
        }

        public async Task<int> Commit()
        {
            return await base.SaveChangesAsync();
        }
    }
}