using System.ComponentModel.DataAnnotations;

namespace BasketballAcademy.Model
{
    public class Admin
    {
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = " ID")]
        public int Id { get; set; }

        [Display(Name = "Full name")]
        public string fullName { get; set; }

        [Display(Name = "Email")]
        public string email { get; set; }

        [Display(Name = "Username ")]
        public string username { get; set; }

        [Display(Name = "Password ")]
        public string password { get; set; }

    }
}
