using System.ComponentModel.DataAnnotations;

namespace WepAppMvc.Models
{
    public class ViewAuthenticationModel
    {

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
