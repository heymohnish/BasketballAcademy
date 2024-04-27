using BasketballAcademy.Extensions;
using BasketballAcademy.Repository.Interface;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace BasketballAcademy.Model
{
    public class Admission :IMapper
    {
        public int Id { get; set; }
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        [Display(Name = "Month")]
        public string ChooseMonths { get; set; }

        [Display(Name = "Coach")]
        public string Coach { get; set; }

        public int CoachID { get; set; }

        [Display(Name = "Parent/Guardian name")]
        public string ParentGuardianName { get; set; }

        [Display(Name = "Parent/Guardian phone number")]
        public string ParentGuardianPhone { get; set; }

        public string Payment { get; set; }

        [Display(Name = "Status")]
        public int status { get; set; }

        public byte[] photo { get; set; }

        public void Map(IDataReader reader)
        {
            Id = reader.GetValue<int>("Id");
            FullName = reader.GetValue<string>("FullName");
            DateOfBirth = reader.GetValue<DateTime>("DateOfBirth");
            Age = reader.GetValue<int>("Age");
            Gender = reader.GetValue<string>("Gender");
            PhoneNumber = reader.GetValue<string>("Phone");
            Email = reader.GetValue<string>("Email");
            ChooseMonths = reader.GetValue<string>("ChooseMonth");
            Coach = reader.GetValue<string>("ChooseCoach");
            CoachID = reader.GetValue<int>("CoachID");
            ParentGuardianName = reader.GetValue<string>("ParentGuardianName");
            ParentGuardianPhone = reader.GetValue<string>("ParentGuardianPhone");
            Payment = reader.GetValue<string>("Payment");
            status = reader.GetValue<int>("Status");
            photo = reader.GetValue<byte[]>("Photo");
        }

    }

    public class AdmissionStatus
    {
        public int Id { get; set; }
        public string status { get; set; }
    }

    public class PlayerList : IMapper
    {
        public int Id { get; set; }
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }
        public byte[] photo { get; set; }

        public void Map(IDataReader reader)
        {
            Id = reader.GetValue<int>("Id");
            FullName = reader.GetValue<string>("FullName");
            Age = reader.GetValue<int>("Age");
            Gender = reader.GetValue<string>("Gender");
            photo = reader.GetValue<byte[]>("Photo");
        }

    }
}
