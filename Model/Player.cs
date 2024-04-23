using System.ComponentModel.DataAnnotations;

namespace BasketballAcademy.Model
{
    public class Player
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


        [Display(Name = "Parent/Guardian name")]
        public string ParentGuardianName { get; set; }

        [Display(Name = "Parent/Guardian phone number")]
        public string ParentGuardianPhone { get; set; }

        public byte[] photo { get; set; }
    }
}
