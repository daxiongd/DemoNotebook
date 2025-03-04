using DemoNotebook.Shared.Contract;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNotebook.Service
{
    public class HttpRestClient
    {
        private readonly string apiUrl;
        protected readonly RestClient _client;

        public HttpRestClient(RestClient client)
        {
            
            _client = client;

        }
        public async Task<ApiResponse> ExecuteAsync(BaseRequest baseRequest)
        {
            var request = new RestRequest(baseRequest.Route, baseRequest.Method);
            request.AddHeader("Content-Type", baseRequest.ContentType);

            if (baseRequest.Parameter != null)
            {
                request.AddParameter(
                    new BodyParameter(
                        "param",
                        JsonConvert.SerializeObject(baseRequest.Parameter),
                        baseRequest.ContentType
                    )
                );
            }

            try
            {
                var response = await _client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<ApiResponse>(response.Content);
                }

                return new ApiResponse
                {
                    Status = false,
                    Message = response.ErrorMessage ?? response.StatusDescription
                };
            }
            catch (Exception ex)
            {
                // 记录异常日志（如果需要）
                Console.WriteLine($"An error occurred: {ex.Message}");

                return new ApiResponse
                {
                    Status = false,
                    Message = $"Request failed: {ex.Message}"
                };
            }


        }
        public async Task<ApiResponse<T>> ExecuteAsync<T>(BaseRequest baseRequest)
        {
            var request = new RestRequest(baseRequest.Route, baseRequest.Method);
            request.AddHeader("Content-Type", baseRequest.ContentType);

            if (baseRequest.Parameter != null)
            {
                request.AddParameter(
                    new BodyParameter(
                        "param",
                        JsonConvert.SerializeObject(baseRequest.Parameter),
                        baseRequest.ContentType
                    )
                );
            }

            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
            }

            return new ApiResponse<T>
            {
                Status = false,
                Message = response.ErrorMessage ?? response.StatusDescription
            };
        }
     
      
        ///// <summary>
        ///// restsharp 106版本前    
        ///// </summary>
        ///// <param name="baseRequest"></param>
        ///// <returns></returns>
        //public async Task<ApiResponse> ExecuteAsync(BaseRequest baseRequest)
        //{
        //    var request = new RestRequest(baseRequest.Method);
        //    request.AddHeader("Content-Type", baseRequest.ContentType);

        //    if (baseRequest.Parameter != null)
        //        request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameter), ParameterType.RequestBody);
        //    client.Options = new Uri(apiUrl + baseRequest.Route);
        //    var response = await client.ExecuteAsync(request);
        //    if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //        return JsonConvert.DeserializeObject<ApiResponse>(response.Content);

        //    else
        //        return new ApiResponse()
        //        {
        //            Status = false,
        //            Result = null,
        //            Message = response.ErrorMessage
        //        };
        //}
        ///// <summary>
        ///// restsharp 106版本前
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="baseRequest"></param>
        ///// <returns></returns>
        //public async Task<ApiResponse<T>> ExecuteAsync<T>(BaseRequest baseRequest)
        //{
        //    var request = new RestRequest(baseRequest.Method);
        //    request.AddHeader("Content-Type", baseRequest.ContentType);

        //    if (baseRequest.Parameter != null)
        //        request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameter), ParameterType.RequestBody);
        //    client.BaseUrl = new Uri(apiUrl + baseRequest.Route);
        //    var response = await client.ExecuteAsync(request); 
        //    if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //        return JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);

        //    else
        //        return new ApiResponse<T>()
        //        {
        //            Status = false, 
        //            Message = response.ErrorMessage
        //        };
        //}

    }
}
