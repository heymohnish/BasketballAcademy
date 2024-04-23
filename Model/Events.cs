using System.ComponentModel.DataAnnotations;

namespace BasketballAcademy.Model
{
    public class Events
    {
        [Display(Name = "Event ID")]
        public int EventID { get; set; }

        [Display(Name = "Event name")]
        public string EventName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime EventDate { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Time")]
        public string EventTime { get; set; }
        public string Venue { get; set; }
        public string Details { get; set; }

        [Display(Name = "Age group")]
        public string AgeGroup { get; set; }

        public string Incharge { get; set; }

        [Display(Name = "Event poster")]
        public byte[] EventImage { get; set; }

        [Display(Name = "Prize details")]
        public string PrizeDetails { get; set; }
        public string Contact { get; set; }

    }
}
