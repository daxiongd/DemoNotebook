using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoNotebook.Shared.Contract
{
   

    // 非泛型别名（可选）
    public class ApiResponse 
    {
        public ApiResponse()
        {
            
        }
        public ApiResponse(string message, bool status = false)
        {
            this.Message = message;
            this.IsSuccess = status;
        }

        public ApiResponse(bool isSuccess, object result)
        {
            this.IsSuccess = isSuccess;
            this.Result = result;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
        public System.Net.HttpStatusCode StatusCode { get; set; }
    }

    // 泛型响应
    public class ApiResponse<T> 
    {
        public ApiResponse()
        {
            
        }
        public ApiResponse(string message, bool status = false)
        {
            this.Message = message;
            this.IsSuccess = status;
        }

        public ApiResponse(bool isSuccess, T result)
        {
            this.IsSuccess = isSuccess;
            this.Result = result;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
        public System.Net.HttpStatusCode StatusCode { get; set; }
    }
}
