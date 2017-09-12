using System;
using System.Linq;
using ChatRoom.Infrastructure.CQS.Command;
using ChatRoom.Infrastructure.CQS.Query;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;

namespace ChatRoom.Infrastructure
{
    public class Bus : IBus
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public Bus(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public TResult Handle<TResult>(ICommand<TResult> command) where TResult : ICommandResult
        {
            var handler = _contextAccessor.HttpContext.RequestServices
                .GetService(typeof(ICommandHandler<,>)
                .MakeGenericType(command.GetType(), typeof(TResult))) as dynamic;

            return handler.Handle((dynamic)command);
        }

        public RequestResponse Process<TResult>(ICommand<TResult> command, ModelStateDictionary modelState) where TResult : ICommandResult
        {
            try
            {
                if (!modelState.IsValid)
                    return SendValidationError(modelState);

                var result = Handle(command);
                return SendSuccess(result);
            }
            catch (Exception e)
            {
                return SendError(e);
            }
        }

        public TResult Handle<TResult>(IQuery<TResult> query) where TResult : IQueryResult
        {
            var handler = _contextAccessor.HttpContext.RequestServices
                .GetService(typeof(IQueryHandler<,>)
                .MakeGenericType(query.GetType(), typeof(TResult))) as dynamic;

            return handler.Handle((dynamic)query);
        }

        public RequestResponse Process<TResult>(IQuery<TResult> query) where TResult : IQueryResult
        {
            try
            {
                var result = Handle(query);
                return SendSuccess(result);
            }
            catch (Exception e)
            {
                return SendError(e);
            }
        }

        private RequestResponse SendValidationError(ModelStateDictionary modelState)
        {
            var validationErrors = modelState
                .Select(x => new ValidationErrorModel
                {
                    Field = x.Key,
                    Error = string.Join(", ", x.Value.Errors.Select(e => e.ErrorMessage))
                });

            return SendResponse(ResponseResult.ModelNotValid, validationErrors, null);
        }

        private RequestResponse SendSuccess(object data)
        {
            return SendResponse(ResponseResult.Ok, data, null);
        }

        private RequestResponse SendError(Exception exception)
        {
            return SendResponse(ResponseResult.Error, null, new []{ exception.Message });
        }

        private RequestResponse SendResponse(ResponseResult result, object data, string[] messages)
        {
            return new RequestResponse
            {
                ResponseResult = result,
                Data = data,
                Messages = messages
            };
        }
    }
}
