using BasketballAcademy.Extensions;
using BasketballAcademy.Repository.Interface;
using System.Data;

namespace BasketballAcademy.Model
{
    public class Registeration:IMapper
    {
        public string Name { get; set; }

        public void Map(IDataReader reader)
        {
            if (reader.FieldCount > 0)
            {            
                Name = reader.GetValue<string>("fullName");
            }
        }
    }
}
