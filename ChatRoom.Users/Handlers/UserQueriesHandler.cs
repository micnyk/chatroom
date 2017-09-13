using System.Threading;
using ChatRoom.Domain.Entities.Users;
using ChatRoom.Infrastructure.CQS.Query;
using ChatRoom.Users.Dtos;
using ChatRoom.Users.Queries;
using Microsoft.AspNetCore.Identity;

namespace ChatRoom.Users.Handlers
{
    public class UserQueriesHandler 
        : IQueryHandler<CheckUserNameUniquenessQuery, CheckUserNameUniquenessResult>
    {
        private readonly IUserStore<ApplicationUser> _userStore;

        public UserQueriesHandler(IUserStore<ApplicationUser> userStore)
        {
            _userStore = userStore;
        }

        public CheckUserNameUniquenessResult Handle(CheckUserNameUniquenessQuery query)
        {
            var result = _userStore.FindByNameAsync(query.UserName.ToUpper(), new CancellationToken()).Result;

            return new CheckUserNameUniquenessResult
            {
                Unique = result == null
            };
        }
    }
}