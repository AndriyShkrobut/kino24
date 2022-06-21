using Microsoft.AspNetCore.Mvc.Infrastructure;
using kino24_like.BL.Interfaces.Logging;
using kino24_like.BL.Services.Logging;
using kino24_like.BL.Interfaces.Comment;
using kino24_like.BL.Services;
using kino24_like.BL.Interfaces.Like;
using kino24_like.Core.Repositories.Contracts;
using kino24_like.Core.Repositories;
using kino24_like.Core.Repositories.Realizations.Base;
using kino24_like.BL.Streaming;
using kino24_like.BL.Serialization;
using kino24_like.BL.Interfaces;

namespace kino24_like.StartupExtensions
{
    public static class AddDependencyExtension
    {
        public static IServiceCollection AddDependency(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped(typeof(ILoggerService<>), typeof(LoggerService<>));

            services.AddSingleton<IActionContextAccessor        , ActionContextAccessor>();
            services.AddScoped<IGlobalLoggerService             , GlobalLoggerService>();

            services.AddScoped<ILikeCommentService       , LikeCommentService>();
            services.AddScoped<ILikeArticleService, LikeArticleService>();
            services.AddTransient<ICommentService     , CommentService>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IUserTokenService, UserTokenService>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddSingleton<IStreamPublisher, StreamPublisher>();
            services.AddSingleton<ISerializer, Serializer>();
            services.AddTransient<IUniqueIdService, UniqueIdService>();


            return services;
        }
    }
}
