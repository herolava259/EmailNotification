using EmailNotification.Application.Responses;
using MediatR;

namespace EmailNotification.Application.Commamds;

public class RemindChangePasswordCommand : IRequest<RemindChangePasswordResponse>
{
    public DateTimeOffset ExpireDate { get; set; }
}
