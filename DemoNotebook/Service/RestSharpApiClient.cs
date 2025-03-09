using System.Net;
using DemoNotebook.Service;
using DemoNotebook.Shared.Contract;
using Newtonsoft.Json;
using RestSharp;

public class RestSharpApiClient :  IApiClient
{
    private readonly RestClient _client;

    public RestSharpApiClient(RestClient client)  {
        _client = client;
    }

    public async Task<ApiResponse<T>> GetAsync<T>(string endpoint, object parameters = null)
    {
        var request = new RestRequest(endpoint);
        AddQueryParameters(request, parameters);
        // 在发送请求前添加调试输出
        var uri = _client.BuildUri(request);
        Console.WriteLine($"Request URL: {uri}");
        var resutl = await SendAsync<T>(request, () => _client.ExecuteGetAsync<T>(request));////泛型ExecuteGetAsync<T> ，restResponse 自动反序列化，如果非泛型则需要手动反序列化
        return resutl;
    }

    public async Task<ApiResponse<T>> PostAsync<T>(string endpoint, object body)
    {
        var request = new RestRequest(endpoint).AddJsonBody(body);
        return await SendAsync(request, () => _client.ExecutePostAsync<T>(request));
    }

    public async Task<ApiResponse<T>> PutAsync<T>(string endpoint, object body)
    {
        var request = new RestRequest(endpoint).AddJsonBody(body);
        return await SendAsync(request, () => _client.ExecutePutAsync<T>(request));
    }

    public async Task<ApiResponse<T>> DeleteAsync<T>(string endpoint)
    {
        var request = new RestRequest(endpoint);
        return await SendAsync(request, () => _client.ExecuteDeleteAsync<T>(request));
    }
    public async Task<ApiResponse> GetAsync(string endpoint, object parameters = null)
    {
        var request = new RestRequest(endpoint);
        AddQueryParameters(request, parameters);
        return await SendAsync(request, () => _client.ExecuteGetAsync(request));////泛型ExecuteGetAsync<T> ，restResponse 自动反序列化，如果非泛型则需要手动反序列化
    }

    public async Task<ApiResponse> PostAsync(string endpoint, object body)
    {
        var request = new RestRequest(endpoint).AddJsonBody(body);
        return await SendAsync(request, () => _client.ExecutePostAsync(request));
    }

    public async Task<ApiResponse> PutAsync(string endpoint, object body)
    {
        var request = new RestRequest(endpoint).AddJsonBody(body);
        return await SendAsync(request, () => _client.ExecutePutAsync(request));
    }

    public async Task<ApiResponse> DeleteAsync(string endpoint)
    {
        var request = new RestRequest(endpoint);
        return await SendAsync(request, () => _client.ExecuteDeleteAsync(request));
    }
    protected void AddQueryParameters(RestRequest request, object parameters)
    {
        if (parameters == null) return;
        foreach (var prop in parameters.GetType().GetProperties())
        {
            var value = prop.GetValue(parameters);
            if (value != null)
            {
                request.AddQueryParameter(prop.Name, value.ToString());
            }
        }
    }
    /// <summary>
    /// 统一处理请求发送与响应解析
    /// </summary>
    public async Task<ApiResponse<T>> SendAsync<T>(
        RestRequest request,
        Func<Task<RestResponse<T>>> sendRequest
    )
    {
        
        try
        {
            var response = await sendRequest();
            if (!string.IsNullOrWhiteSpace(response.Content) && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
               
                  return JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
            }
            else
            {
                return new ApiResponse<T>
                {
                    IsSuccess = false,
                    Message = "Empty response content",
                    StatusCode = response.StatusCode
                };
            }
        }
        catch (Exception ex)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                Message = ex.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
    // 在 HttpRestClient 中添加非泛型版本
    public async Task<ApiResponse> SendAsync(
        RestRequest request,
        Func<Task<RestResponse>> sendRequest // 非泛型委托
    )
    {

        try
        {
            var response = await sendRequest();
            if (!string.IsNullOrWhiteSpace(response.Content) || response.StatusCode == System.Net.HttpStatusCode.OK)
            { return JsonConvert.DeserializeObject<ApiResponse>(response.Content); }
            else
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Empty response content",
                    StatusCode = response.StatusCode
                };
            }
        }
        catch (Exception ex)
        {
            return new ApiResponse
            {
                IsSuccess = false,
                Message = ex.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

}