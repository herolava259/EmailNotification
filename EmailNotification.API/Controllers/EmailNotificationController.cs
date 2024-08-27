
using EmailNotification.Application.Commamds;
using EmailNotification.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace EmailNotification.API.Controllers;

public class EmailNotificationController: ApiController
{
    private readonly IMediator _mediator;
    private readonly ILogger<EmailNotificationController> _logger;

    public EmailNotificationController(IMediator mediator, ILogger<EmailNotificationController> logger)
    {
        this._mediator = mediator;
        this._logger = logger;
    }

    [HttpGet("remindchangepassword/{simulateTime}", Name = "RemindChangePassword")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<RemindChangePasswordResponse>> RemindChangePassword(string simulateTime)
    {
        simulateTime = HttpUtility.UrlDecode(simulateTime);
        var check = DateTimeOffset.TryParse(simulateTime, out var expiredDate);

        if(!check)
        {
            _logger.LogError("parameter query is invalid");
            return BadRequest(new RemindChangePasswordResponse { Result = false, TotalOfReminder = 0 });
        }
        var cmd = new RemindChangePasswordCommand
        {
            ExpireDate = expiredDate
        };

        _logger.LogInformation("Start proceeding RemindChangePassWord API");
        var response = await _mediator.Send(cmd);
        _logger.LogInformation("Finish proceeding RemindChangePassWord API");

        if (response.Result)
            return BadRequest(response);

        return Ok(response);
    }
}
