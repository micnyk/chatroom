using System;
using System.Linq;
using ChatRoom.Infrastructure.CQS.Command;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ChatRoom.Infrastructure
{
    public class RequestProcessor : IRequestProcessor
    {
        public RequestResponse Process<TResult>(ICommand<TResult> command, dynamic handler, ModelStateDictionary modelState) where TResult : ICommandResult
        {
            try
            {
                if (!modelState.IsValid)
                    return SendValidationError(modelState);

                var result = handler.Handle((dynamic)command);

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
