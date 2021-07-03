using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RockContent.Data;
using RockContent.Data.Repositories;
using RockContent.Domain.Commands;
using RockContent.Domain.Repositories;
using RockContent.Shared;
using RockContent.Shared.Commands;
using RockContent.Shared.Data;
using RockContent.Shared.Mediator;

namespace RocKContent.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RocKContent.Api", Version = "v1" });
            });

            services.AddScoped<IRequestHandler<MarkLikedInArticleCommand, CommandResult>, ArticleLikeCommandHandler>();
            services.AddScoped<IRequestHandler<UncheckLikedInArticleCommand, CommandResult>, ArticleLikeCommandHandler>();

            services.AddDbContext<RockContentContext>(opt => opt.UseInMemoryDatabase(Settings.RockContentDatabaseName));
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IArticleRepository, ArticleRepository>();

            services.AddMediatR(typeof(Startup));
            services.AddScoped<IMediatorHandler, MediatorHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RocKContent.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Seed data to tests
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetService<IDbInitializer>();
                dbInitializer.SeedData();
            }
        }
    }
}
