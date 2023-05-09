using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Projections.UserService.Services;

namespace BlazorSozluk.Projections.UserService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private Services.UserService _userService;
    private readonly EmailService _emailService;

    public Worker(ILogger<Worker> logger, Services.UserService userService, EmailService emailService)
    {
        _logger = logger;
        _userService = userService;
        _emailService = emailService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        QueueFactory.CreateBasicConsumer()
             .EnsureExchange(sozlukConstatns.UserExchangeName)
             .EnsureQueue(sozlukConstatns.UserEmailChangedQueueName, sozlukConstatns.UserExchangeName)
             .Recive<UserEmailChangedEvent>(user =>
             {
                 var confirmationId = _userService.CreateEmailConfirmation(user).GetAwaiter().GetResult();

                 var link = _emailService.GenerateConfirmationLink(confirmationId);

                 _emailService.SendEmail(user.NewEmailAddress,link).GetAwaiter().GetResult();

             })
             .StartConsuming(sozlukConstatns.UserEmailChangedQueueName);
    }
}
