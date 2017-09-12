using ChatRoom.Infrastructure.CQS.Query;
using ChatRoom.Users.Dtos;

namespace ChatRoom.Users.Queries
{
    public class CheckUserNameUniquenessQuery : IQuery<CheckUserNameUniquenessResult>
    {
        public string UserName { get; set; }
    }
}