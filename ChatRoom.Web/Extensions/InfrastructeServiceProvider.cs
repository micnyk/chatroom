using ChatRoom.Infrastructure;
using ChatRoom.Infrastructure.CQS.Command;
using ChatRoom.Infrastructure.CQS.Query;
using ChatRoom.Users.Commands;
using ChatRoom.Users.Dtos;
using ChatRoom.Users.Handlers;
using ChatRoom.Users.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoom.Web.Extensions
{
    public static class InfrastructeServiceProvider
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IBus, Bus>();

            services.AddTransient<ICommandHandler<RegisterCommand, RegisterResult>, UserCommandsHandler>();
            services.AddTransient<ICommandHandler<SignInCommand, SignInResult>, UserCommandsHandler>();
            services.AddTransient<ICommandHandler<SignOutCommand, SignOutResult>, UserCommandsHandler>();
            services.AddTransient<IQueryHandler<CheckUserNameUniquenessQuery, CheckUserNameUniquenessResult>, UserQueriesHandler>();

            return services;
        }
    }
}
