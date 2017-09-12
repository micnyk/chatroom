namespace ChatRoom.Infrastructure.CQS.Command
{
    public interface ICommand<TResult> where TResult : ICommandResult
    {
    }
}
