using BaseService.APIService;

namespace BaseService.HttpService
{
    public interface IApiService<TData, TResult>
    {
        ApiResponse<TResult> responseModel { get; set; }

        Task<ApiResponse<TResult>> SendAsync(ApiRequest<TData> apiRequest);
    }
}
