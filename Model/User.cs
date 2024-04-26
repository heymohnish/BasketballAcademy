using BasketballAcademy.Extensions;
using BasketballAcademy.Repository.Interface;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace BasketballAcademy.Model
{
    public class User:IMapper

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

        public void Map(IDataReader reader)
        {
            if (reader.FieldCount > 0)
            {
                id = reader.GetValue<int>("ID");
                FullName = reader.GetValue<string>("fullName");
                Email = reader.GetValue<string>("Email");
                username = reader.GetValue<string>("username");
            }
        }

    }

    public class AuthUser
    {
        [Required(ErrorMessage = "User Name is required")]
        public string username { get; set; }
        public string Key { get; set; }
    }
}
