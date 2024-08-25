using BaseService.APIService;

namespace BaseService.ApiService;

public interface IApiService<TData, TResult>
{
    ApiResponse<TResult> responseModel { get; set; }

    Task<ApiResponse<TResult>> SendAsync(ApiRequest<TData> apiRequest);
}
