using BaseService.HttpService;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using static BaseService.APIService.APIEnum;

namespace BaseService.APIService
{
    public class ApiService<TData, TResult> : IApiService<TData, TResult>
    {
        private readonly IHttpClientFactory _httpClient;

        public ApiService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
            responseModel = new();
        }

        public ApiResponse<TResult> responseModel { get; set; }

        public async Task<ApiResponse<TResult>> SendAsync(ApiRequest<TData> apiRequest)
        {
            try
            {
                var client = _httpClient.CreateClient("InternalService");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8,
                                                        "application/json");
                }

                switch (apiRequest.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;

                }


                var apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();

                try
                {
                    

                    if (apiResponse.StatusCode == HttpStatusCode.BadRequest ||
                       apiResponse.StatusCode == HttpStatusCode.NotFound || 
                       apiResponse.StatusCode == HttpStatusCode.BadGateway)
                    {
                        responseModel.IsSuccess = false;
                        responseModel.Result = null;
                        responseModel.StatusCode = apiResponse.StatusCode;
                    }
                    else
                    {
                        var response = JsonConvert.DeserializeObject<TResult>(apiContent);
                        responseModel.IsSuccess = true;
                        responseModel.Result = response;
                        responseModel.StatusCode = apiResponse.StatusCode;
                    }


                }
                catch (Exception ex)
                {
                    responseModel.IsSuccess = false;
                    responseModel.Result = null;
                    responseModel.ErrorMessages = new() { ex.ToString() };
                    
                }

                return responseModel;
            }
            catch (Exception ex)
            {
                responseModel.IsSuccess = false;
                responseModel.Result = null;
                responseModel.ErrorMessages = new() { ex.ToString() };
                return responseModel;
            }
        }
    }
}
