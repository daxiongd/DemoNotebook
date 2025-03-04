using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoNotebook.Shared.Contract
{
   

    // 非泛型别名（可选）
    public class ApiResponse 
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }

    // 泛型响应
    public class ApiResponse<T> 
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
