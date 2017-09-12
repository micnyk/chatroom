namespace ChatRoom.Infrastructure.CQS.Query
{
    public interface IQueryHandler<in TQuery, out TResult> 
        where TQuery : IQuery<TResult>
        where TResult : IQueryResult
    {
        TResult Handle(TQuery query);
    }
}
