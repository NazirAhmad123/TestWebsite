using System.ComponentModel.DataAnnotations;

namespace WebsiteDemo.Models
{
    public class ContactModel
    {
        [Required]
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name:")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address:")]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Message:")]
        public string Message { get; set; }
    }
}