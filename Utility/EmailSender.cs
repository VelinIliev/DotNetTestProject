using Microsoft.AspNetCore.Identity.UI.Services;
namespace Utility;

public class EmailSender : IEmailSender 
{
    public Task SendEmailAsync(string email, string subject, string htmlMessager)
    {
        // logic to send email
        return Task.CompletedTask;
    }
}