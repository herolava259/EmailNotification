using BaseService.ApiService;

namespace EmailNotification.CronJobWorker.RemindChangePassword
{
    public interface IRemindChangePasswordService: IApiService<object, RemindChangePasswordResponse>
    {
    }
}
