using BasketballAcademy.Extensions;
using BasketballAcademy.Repository.Interface;
using System.Data;

namespace BasketballAcademy.Model
{
    public class Contact : IMapper
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        public void Map(IDataReader reader)
        {
            if (reader.FieldCount > 0)
            {
                Id = reader.GetValue<int>("ID");
                Name = reader.GetValue<string>("Name");
                Phone = reader.GetValue<string>("Phone");
                Email = reader.GetValue<string>("Email");
                Message = reader.GetValue<string>("Message");
            }
        }
    }
}
