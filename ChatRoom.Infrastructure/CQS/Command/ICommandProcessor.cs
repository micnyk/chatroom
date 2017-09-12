namespace ChatRoom.Infrastructure.CQS.Command
{
    public interface ICommandProcessor
    {
        TResult Process<TResult>(ICommand<TResult> command) where TResult : ICommandResult;
    }
}
