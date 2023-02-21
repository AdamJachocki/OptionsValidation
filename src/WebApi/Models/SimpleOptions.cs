using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class SimpleOptions
    {
        [EmailAddress]
        public string SenderEmail { get; set; }
        [Required]
        public string SmtpAddress { get; set; }
    }
}
