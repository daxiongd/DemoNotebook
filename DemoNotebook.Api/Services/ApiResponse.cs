namespace DemoNotebook.Api.Services
{
    public class ApiResponse
    {
        public ApiResponse(object result,bool status)
        {
            this.Status = status;
            this.Result = result;
        }
        public ApiResponse(string   message,bool    status= false)
        {
            this.Message = message;
            this.Status = status;
        }
        public string  Message { get; set; }

        public bool Status { get; set; }
        public object Result { get; set; }
    }
}
