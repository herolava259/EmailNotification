using BaseService.APIService;
using BaseService.HttpService;
using Coravel.Invocable;

namespace EmailNotification.CronJobWorker.RemindChangePassword
{
    public class RemindChangePasswordInvocable : IInvocable
    {
        private readonly IApiService<object, bool> _apiService;
        private readonly string? _url ;
        private readonly ILogger<RemindChangePasswordInvocable> _logger;    

        public RemindChangePasswordInvocable(IApiService<object,bool> apiService, 
                                             IConfiguration configuration,
                                             ILogger<RemindChangePasswordInvocable> logger)
        {
            this._apiService = apiService;
            this._url = configuration.GetValue<string>("ServiceUrls:EmailNotification");
            this._logger = logger;
        }
        public async Task Invoke()
        {
            _logger.LogInformation("Begin RemindChangePasswordJob");
            var apiRequest = new ApiRequest<object>
            {
                ApiType = APIEnum.ApiType.GET,
                Url = $"{this._url}/api/useraccount/remindchangepassword"
            };

            var response = await _apiService.SendAsync(apiRequest);

            if(response == null || !response.IsSuccess || !response.Result)
            {
                _logger.LogError($"Something went wrong while send request remind change password. {String.Join('\n', response!.ErrorMessages)}");
            }

            _logger.LogInformation("Complete remind change password request");

        }
    }
}
