using System;
namespace WebApi_Application.Models
{
    public class EmailOptions
    {
        public const string EmailConfig = "EmailConfiguration";
        public required string From { get; set; }
        public required string SmtpServer { get; set; }
        public int Port { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}

