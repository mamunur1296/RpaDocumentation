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
    }
}
