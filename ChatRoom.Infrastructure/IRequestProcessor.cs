using ChatRoom.Infrastructure.CQS.Command;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ChatRoom.Infrastructure
{
    public interface IRequestProcessor
    {
        RequestResponse Process<TResult>(ICommand<TResult> command, dynamic handler, ModelStateDictionary modelState) where TResult : ICommandResult;
    }
}
