using ChatRoom.Infrastructure.CQS.Command;
using ChatRoom.Infrastructure.CQS.Query;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ChatRoom.Infrastructure
{
    public interface IBus
    {
        TResult Handle<TResult>(ICommand<TResult> command) where TResult : ICommandResult;
        RequestResponse Process<TResult>(ICommand<TResult> command, ModelStateDictionary modelState) where TResult : ICommandResult;

        TResult Handle<TResult>(IQuery<TResult> query) where TResult : IQueryResult;
        RequestResponse Process<TResult>(IQuery<TResult> query) where TResult : IQueryResult;
    }
}
