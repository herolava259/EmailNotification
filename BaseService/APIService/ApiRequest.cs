

using static BaseService.APIService.APIEnum;

namespace BaseService.APIService
{
    public class ApiRequest<TData>
        
    {
        public ApiType ApiType { get; set; } = ApiType.GET;

        public string Url { get; set; }

        public TData? Data { get; set; }

    }
}
