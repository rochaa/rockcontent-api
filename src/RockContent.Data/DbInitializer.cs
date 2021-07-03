using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using RockContent.Domain.Entities;
using RockContent.Shared.Data;

namespace RockContent.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<RockContentContext>())
                {
                    if (!context.Articles.Any())
                    {
                        var article1 = new Article("Saiba como entrevistar um cliente", "Saber como entrevistar um cliente é uma maneira tradicional, porém poderosa, de garantir a competitividade da sua empresa e elevar a experiência dos consumidores com a sua marca. A estratégia serve para obter insights, tanto para valorizar as forças e as potencialidades do negócio quanto para implementar mudanças ou combater falhas e fraquezas.", "Anelise Margotti", DateTime.Now.AddDays(-2));
                        var article2 = new Article("Será que você sabe realmente o que é um bom ROI?", "Determinar o que é um bom ROI é importante para que uma empresa possa obter um resultado mais realista desse indicador, pois os valores mensurados variam conforme o setor de negócio ou o investimento realizado.", "Ivan de Souza", DateTime.Now.AddDays(-10));
                        var article3 = new Article("Confira as 7 principais técnicas para otimização de web sites", "A otimização de web sites é fundamental para que qualquer negócio fortaleça sua presença digital e consiga se destacar dentro de um cenário cada vez mais competitivo. Com as técnicas certas, suas páginas vão alcançar uma performance muito melhor e, consequentemente, os resultados do negócio como um todo vão melhorar.", "Gabriel Camargo", DateTime.Now.AddDays(-30));

                        context.Articles.AddRange(article1, article2, article3);
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}