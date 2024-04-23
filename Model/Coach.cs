using System.ComponentModel.DataAnnotations;

namespace BasketballAcademy.Model
{
    public class Coach
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
    }
}
