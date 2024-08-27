using BaseService.APIService;
using BaseService.ApiService;
using Coravel.Invocable;
using System.Web;

namespace EmailNotification.CronJobWorker.RemindChangePassword
{
    public class RemindChangePasswordInvocable : IInvocable
    {
        private readonly IRemindChangePasswordService _apiService;
        private readonly string? _url ;
        private readonly ILogger<RemindChangePasswordInvocable> _logger;    

        public RemindChangePasswordInvocable(IRemindChangePasswordService apiService, 
                                             IConfiguration configuration,
                                             ILogger<RemindChangePasswordInvocable> logger)
        {
            this._apiService = apiService;
            this._url = configuration.GetValue<string>("ServiceUrls:EmailNotification");
            this._logger = logger;
        }
        public async Task Invoke()
        {
            
            var url = $"{this._url}/api/v1/EmailNotification/remindchangepassword/{HttpUtility.UrlEncode(DateTimeOffset.UtcNow.ToString("MM/dd/yyyy"))}";
            _logger.LogInformation("Begin RemindChangePasswordJob");
            
            _logger.LogInformation(url);
            var apiRequest = new ApiRequest<object>
            {
                ApiType = APIEnum.ApiType.GET,
                Url = url
            };
            //{DateTimeOffset.UtcNow.ToString("MM/dd/yyyy")}
            var response = await _apiService.SendAsync(apiRequest);

            if(response == null || !response.IsSuccess || !response.Result!.Result)
            {
                _logger.LogError($"Something went wrong while send request remind change password. {String.Join('\n', response!.ErrorMessages)}");
            }

            _logger.LogInformation("Complete RemindChangePassword request");

        }
    }
}
