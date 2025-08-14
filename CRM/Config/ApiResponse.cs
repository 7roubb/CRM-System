using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace CRM.Config
{
    public class ApiResponse<T>
    {
        public T Content { get; set; }
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }

        public static ApiResponse<T> Success(T content, HttpStatusCode status, string message = null)
        {
            return new ApiResponse<T>
            {
                Content = content,
                Status = status,
                Message = message
            };
        }

        public static ApiResponse<T> Error(string message, HttpStatusCode status)
        {
            return new ApiResponse<T>
            {
                Content = default,
                Status = status,
                Message = message
            };
        }

        public static ApiResponse<T> Of(T content, HttpStatusCode status, string message = null)
        {
            return Success(content, status, message);
        }

   
    }
}