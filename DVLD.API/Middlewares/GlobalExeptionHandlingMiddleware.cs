using DVLD.Application.Common.ApiResponse;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace DVLD.API.Middlewares
{
    public class GlobalExeptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExeptionHandlingMiddleware> _logger;

        public GlobalExeptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExeptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                _logger.LogError(error, error.Message);
                await HandleExceptionAsync(context, error);
            }
        }



        private async static Task HandleExceptionAsync(HttpContext context, Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = new ApiResponse<string>() { Succeeded = false, Errors = [] };

            //TODO:: cover all validation errors
            switch (error)
            {
                case UnauthorizedAccessException e:
                    // custom application error
                    responseModel.Message = error.Message;
                    responseModel.StatusCode = HttpStatusCode.Unauthorized;
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;

                case ValidationException e:
                    // custom validation error
                    responseModel.Errors.Add(error.Message);
                    responseModel.StatusCode = HttpStatusCode.UnprocessableEntity;
                    response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    break;
                case KeyNotFoundException e:
                    // not found error
                    responseModel.Message = error.Message; ;
                    responseModel.StatusCode = HttpStatusCode.NotFound;
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case Exception e:
                    if (e.GetType().ToString() == "ApiException")
                    {
                        responseModel.Message += e.Message;
                        responseModel.Message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;
                        responseModel.StatusCode = HttpStatusCode.BadRequest;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }
                    responseModel.Message = e.Message;
                    responseModel.Message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;

                    responseModel.StatusCode = HttpStatusCode.InternalServerError;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;

                default:
                    // unhandled error
                    responseModel.Message = error.Message;
                    responseModel.StatusCode = HttpStatusCode.InternalServerError;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(responseModel);

            await response.WriteAsync(result);
        }
    }
}
