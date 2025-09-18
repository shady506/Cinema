using System.ComponentModel.DataAnnotations;

namespace Cinema.ViewModels
{
    public class UpdatePersonalinfoVM
    {
        public string Id { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
      
        public string PhoneNumber { get; set; } = string.Empty;

        public string? Streat { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }

        public IFormFile? ProfileImage { get; set; } 
        public string? ProfilePicture { get; set; } 


        public string? CurrentPassword { get; set; } 
        public string? NewPassword { get; set; } 
        public string? ConfirmPassword { get; set; } 
    }
}
