using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
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

            AuthenticationService(services);
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
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
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

        private static void AuthenticationService(IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(Settings.SecretJWT);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
