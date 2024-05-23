using System.Net;


namespace Project.Application.Response
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public HttpStatusCode Status { get; set; }
        public Response()
        {
            Success = true;
        }
    }
}
