namespace ChatRoom.Infrastructure.CQS.Command
{
    public interface ICommandHandler<in TCommand, out TResult> 
        where TCommand : ICommand<TResult>
        where TResult : ICommandResult
    {
        TResult Handle(TCommand command);
    }
}
