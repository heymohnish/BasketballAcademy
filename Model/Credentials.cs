using BasketballAcademy.Extensions;
using BasketballAcademy.Repository.Interface;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace BasketballAcademy.Model
{
    public class Credentials
    {


        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class LoginResponse:IMapper
    {

        public string id { get; set; }

        public string role { get; set; }

        public void Map(IDataReader reader)
        {
            if (reader.FieldCount > 0)
            {
                id = reader.GetValue<string>("ID");
                role = reader.GetValue<string>("role");
            }
        }
    
}
}
