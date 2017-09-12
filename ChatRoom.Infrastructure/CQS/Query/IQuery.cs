namespace ChatRoom.Infrastructure.CQS.Query
{
    public interface IQuery<TResult> where TResult : IQueryResult
    {
    }
}
