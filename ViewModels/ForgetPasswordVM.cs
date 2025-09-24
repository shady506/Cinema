using System.ComponentModel.DataAnnotations;

namespace Cinema.ViewModels
{
    public class ForgetPasswordVM
    {
        public int Id { get; set; }

        [Required]
        public string EmailOrUserName { get; set; } = string.Empty;
    }
}
