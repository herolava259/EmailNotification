using BaseService.ApiService;
using BaseService.APIService;

namespace EmailNotification.CronJobWorker.RemindChangePassword
{
    public class RemindChangePasswordService : ApiService<object, RemindChangePasswordResponse>, IRemindChangePasswordService
    {
        public RemindChangePasswordService(IHttpClientFactory httpClient) : base(httpClient)
        {
        }
    }
}
