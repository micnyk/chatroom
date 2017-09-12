namespace ChatRoom.Infrastructure.CQS.Command
{
    public interface ICommandHandler<TCommand, TResult> 
        where TCommand : ICommand<TResult>
        where TResult : ICommandResult
    {
        TResult Handle(TCommand command);
    }
}
