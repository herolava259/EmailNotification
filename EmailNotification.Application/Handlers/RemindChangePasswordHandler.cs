using EmailNotification.Application.Commamds;
using EmailNotification.Application.Responses;
using EmailNotification.Core.Entities;
using EmailNotification.Core.Repositories;
using EmailNotification.EmailService;
using MediatR;
using Microsoft.Extensions.Logging;
using EStatus = EmailNotification.Core.Enums.UserAccountEnum.EStatus;

namespace EmailNotification.Application.Handlers
{
    public class RemindChangePasswordHandler 
        : IRequestHandler<RemindChangePasswordCommand, 
                            RemindChangePasswordResponse>
    {
        private readonly IEmailService _emailService;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly ILogger<RemindChangePasswordHandler> _logger;

        public RemindChangePasswordHandler(IEmailService emailService, 
                                            IUserAccountRepository userAccountRepository,
                                            ILogger<RemindChangePasswordHandler> logger)
        {
            this._emailService = emailService;
            this._userAccountRepository = userAccountRepository;
            this._logger = logger;
        }
        public async Task<RemindChangePasswordResponse> Handle(RemindChangePasswordCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("begin handle method of RemindChangePasswordHandler");
            if (!(await _userAccountRepository.AnyAsync(c => c.LastUpdatePassword < request.ExpireDate.AddMonths(6)
                                                            && c.Status == EStatus.Normal)))
            {
                _logger.LogDebug("End handle method of RemindChangePasswordHandler");
                return new()
                {
                    Result = true,
                    TotalOfReminder = 0
                };
            }
            var accountsNotChange = await _userAccountRepository.GetUserAccountsNeedToChangePassword(request.ExpireDate);

            var result = await _userAccountRepository.RequireToChangePassword(accountsNotChange);

            if(!result)
            {
                _logger.LogDebug("End handle method of RemindChangePasswordHandler");
                return new()
                {
                    Result = false,
                    TotalOfReminder = 0
                };
            }
            var totalOfReminder = await SendRemindingChangePwdEmail(accountsNotChange);
            
            _logger.LogDebug("End handle method of RemindChangePasswordHandler");

            return new()
            {
                Result = accountsNotChange.Count == totalOfReminder,
                TotalOfReminder = totalOfReminder
            };
        }

        private async Task<int> SendRemindingChangePwdEmail(IEnumerable<UserAccount> accounts)
        {
            int totalOfReminder = 0;
            foreach(var chunk in accounts.Chunk(10))
            {
                List<Task<bool>> sendingEmailTasks = chunk.Select(c =>
                {
                    var message = new Message(to: new string[] { c.Email}, 
                        subject: "Your Account need to be changed password",
                        content: $"Hi {c.Profile!.FirstName} {c.Profile!.LastName}, you need to change password because account password's exired");

                    return _emailService.SendEmailAsync(message);
                }).ToList();

                await Task.WhenAll(sendingEmailTasks);

                foreach (var item in sendingEmailTasks.Zip(chunk))
                {
                    var result = await item.First;

                    if (!result)
                    {
                        _logger.LogError(@$"Something went wrong while sending change password reminder to 
                                             {item.Second.Profile!.FirstName} {item.Second.Profile!.LastName}
                                            has email {item.Second.Email}");
                        
                    }
                    else
                    {
                        _logger.LogInformation(@$"sending change password reminder to 
                                             {item.Second.Profile!.FirstName} {item.Second.Profile!.LastName}
                                            has email {item.Second.Email} is successful");
                        totalOfReminder++;
                    }
                }

            }

            return totalOfReminder;
        }
    }
}
