using System;
using System.Net;

namespace escout.Models
{
    public class HttpResponse
    {
        public int StatusCode { get; set; }
        public object Content { get; set; }

        public HttpResponse() { }

        public HttpResponse(Exception ex)
        {
            StatusCode = 1;
            Content = ex.Message;
        }

        public HttpResponse(HttpStatusCode statusCode, object content)
        {
            StatusCode = (int)statusCode;
            Content = content;
        }
    }
}
