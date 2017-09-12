using ChatRoom.Infrastructure.CQS.Query;

namespace ChatRoom.Users.Dtos
{
    public class CheckUserNameUniquenessResult : IQueryResult
    {
        public bool Unique { get; set; }
    }
}