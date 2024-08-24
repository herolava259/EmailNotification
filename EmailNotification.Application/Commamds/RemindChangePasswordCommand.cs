using MediatR;

namespace EmailNotification.Application.Commamds;

public class RemindChangePasswordCommand : IRequest<Unit>
{
    public DateTimeOffset AfterDate { get; set; }
}
