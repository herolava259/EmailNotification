namespace EmailNotification.CronJobWorker.RemindChangePassword
{
    public class RemindChangePasswordResponse
    {
        public bool Result { get; set; }

        public int TotalOfReminder { get; set; } = 0;
    }
}
