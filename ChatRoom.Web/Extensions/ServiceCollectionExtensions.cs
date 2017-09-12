using ChatRoom.Infrastructure;
using ChatRoom.Infrastructure.CQS.Command;
using ChatRoom.Users.Commands;
using ChatRoom.Users.Dtos;
using ChatRoom.Users.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoom.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IRequestProcessor, RequestProcessor>();

            services.AddTransient<ICommandHandler<CreateUserCommand, CreateUserResult>, UserCommandsHandler>();

            return services;
        }
    }
}
