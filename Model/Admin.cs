using BasketballAcademy.Extensions;
using BasketballAcademy.Repository.Interface;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;
using System.Reflection;

namespace BasketballAcademy.Model
{
    public class Admin : IMapper
    {
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

        public void Map(IDataReader reader)
        {
            if (reader.FieldCount > 0)
            {
                Id = reader.GetValue<int>("ID");
                fullName = reader.GetValue<string>("fullName");
                email = reader.GetValue<string>("email");
                username = reader.GetValue<string>("username");
            }
        }
    }


}

