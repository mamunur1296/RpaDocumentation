using System.Net;


namespace Project.Application.Response
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public Response()
        {
            Success = true;
        }

        public void SetupSuccessResponse(Response<string> response, Guid customerId,string message)
        {
            response.Data = message;
            response.Success = true;
            response.StatusCode = HttpStatusCode.OK;
        }

        public void SetupFailureResponse(Response<string> response, HttpStatusCode statusCode, string errorMessage)
        {
            response.Success = false;
            response.StatusCode = statusCode;
            response.ErrorMessage = errorMessage;
        }
    }
}
