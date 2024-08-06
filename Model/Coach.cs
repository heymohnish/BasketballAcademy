using BasketballAcademy.Extensions;
using BasketballAcademy.Repository.Interface;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Reflection;

namespace BasketballAcademy.Model
{
    public class Coach : IMapper
    {
        public int Id { get; set; }

        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Age")]
        public int Age { get; set; }

        public string Gender { get; set; }

        public string PrimarySkill { get; set; }
        public string Address { get; set; }

        [Display(Name = "Contact")]

        public string PhoneNumber { get; set; }


        public string Email { get; set; }

        [Display(Name = "Experience")]

        public int Experience { get; set; }

        [Display(Name = "Photo")]
        public byte[] photo { get; set; }

        [Display(Name = "ID proof")]
        public byte[] idproof { get; set; }

        [Display(Name = "Certificate")]
        public byte[] CertificateProof { get; set; }

        public void Map(IDataReader reader)
        {

            if (reader.FieldCount > 0)
            {
                DateOfBirth = reader.GetValue<DateTime>("DateOfBirth");
                Id = reader.GetValue<int>("ID");
                Age = reader.GetValue<int>("age");
                FullName = reader.GetValue<string>("fullName");
                Gender = reader.GetValue<string>("Gender");
                PrimarySkill = reader.GetValue<string>("PrimarySkill");
                Address = reader.GetValue<string>("Address");
                PhoneNumber = reader.GetValue<string>("phone");
                Email = reader.GetValue<string>("Email");
                Experience = reader.GetValue<int>("Experience");
                photo = reader.GetValue<byte[]>("Photo");
                idproof = reader.GetValue<byte[]>("IDProof");
                CertificateProof = reader.GetValue<byte[]>("CertificateProof");

            }
        }
    }

public class CoachList:IMapper
    {
        public int Id { get; set; }

        [Display(Name = "Full name")]
        public string FullName { get; set; }
        public byte[] photo { get; set; }
        public int Experience { get; set; }
        public string PrimarySkill { get; set; }

        public void Map(IDataReader reader)
        {
            Id = reader.GetValue<int>("ID");
            FullName = reader.GetValue<string>("fullName");
            PrimarySkill = reader.GetValue<string>("PrimarySkill");
            Experience = reader.GetValue<int>("Experience");
            photo = reader.GetValue<byte[]>("Photo");
        }
    }
}
