using ChatRoom.Infrastructure.CQS.Command;

namespace ChatRoom.Infrastructure
{
    public class RequestProcessor : IRequestProcessor
    {
        private ICommandProcessor _commandProcessor;

        public RequestProcessor(ICommandProcessor commandProcessor)
        {
            _commandProcessor = commandProcessor;
        }

        public TResult Process<TResult>(ICommand<TResult> command) where TResult : ICommandResult
        {
            return _commandProcessor.Process(command);
        }
    }
}
