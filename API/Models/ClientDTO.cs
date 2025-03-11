using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class ClientDTO
    {
        [Required(ErrorMessage = "Primeiro nome é obrigatório")]
        public string FirstName { get; set; } = "";

        [Required(ErrorMessage = "Último nome é obrigatório")]
        public string LastName { get; set; } = "";

        [Required(ErrorMessage = "Email é obrigatório")] 
        [EmailAddress]
        public string Email { get; set; } = "";

        [Phone]
        public  string? Phone { get; set; }
        public string? Address { get; set; }

        [Required(ErrorMessage = "Status é obrigatório")]
        public string Status { get; set; } = ""; //New, permanent, occasional, inactive
    }

}
