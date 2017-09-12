namespace ChatRoom.Infrastructure.CQS.Command
{
    public class CommandProcessor : ICommandProcessor
    {
        public TResult Process<TResult>(ICommand<TResult> command) where TResult : ICommandResult
        {
            var handler = AppServiceProvider.Provider.GetService(typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult)));

            return default(TResult);
        }
    }
}
