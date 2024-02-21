using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using project_get_discount_back._1_Domain.Interfaces;
using project_get_discount_back._1_Domain.Service.Usuario;
using project_get_discount_back.Context;
using project_get_discount_back.Helpers;
using project_get_discount_back.Interfaces;
using project_get_discount_back.Repositories;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

namespace project_get_discount_back._1_Domain.Config
{
    [ExcludeFromCodeCoverage]
    public static class ServiceColletionExtensions
    {
        public static IServiceCollection ConfigDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<DataContext>(options =>
                            options.UseNpgsql(configuration.GetConnectionString("WebApiDatabase")));
        }

        public static IServiceCollection ConfigMediatR(this IServiceCollection services) =>
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        public static IServiceCollection ConfigEndPonts(this IServiceCollection services) =>
            services.AddEndpointsApiExplorer();

        public static IMvcBuilder ConfigControlles(this IServiceCollection services) =>
            services.AddControllers();

        public static IServiceCollection ConfigSwagger(this IServiceCollection services) =>
            services.AddSwaggerGen(c =>
            {
                 c.SwaggerDoc("v1", new OpenApiInfo
                     {
                        Title = "Project Get Discount Back",
                        Version = "v1",
                        Contact = new OpenApiContact
                        {
                        Name = "Alexandre",
                        Email = "g.aleprojetos@gmail.com"
                        }
                 });

                // Adicione o caminho do arquivo XML de documentação aqui
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

        public static IServiceCollection ConfigServices(this IServiceCollection services) =>
            services.AddScoped<TokenService>()
                    .AddScoped<IUserRepository, UserRepository>()
                    .AddScoped<IUserService, UserService>()
                    .AddScoped<IUnitOfWork, UnitOfWork>();

        public static IServiceCollection ConfigAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var keyString = configuration.GetSection("Jwt:key")?.Value;
            if (string.IsNullOrEmpty(keyString))
            {
                throw new ApplicationException("Chave JWT não encontrada no arquivo de configuração.");
            }

            var key = Encoding.ASCII.GetBytes(keyString);

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

            return services;
        }
    }
}
