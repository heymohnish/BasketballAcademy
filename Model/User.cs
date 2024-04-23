using System.ComponentModel.DataAnnotations;

namespace BasketballAcademy.Model
{
    public class User

    {
        [Display(Name = "ID")]
        public int id { get; set; }

        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [Display(Name = "User name")]
        public string username { get; set; }
        public string Email { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

    }

    public class AuthUser
    {
        [Required(ErrorMessage = "User Name is required")]
        public string username { get; set; }
        public string Key { get; set; }
    }
}
