using BasketballAcademy.Extensions;
using BasketballAcademy.Repository.Interface;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace BasketballAcademy.Model
{

    public class Events:IMapper
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

        public void Map(IDataReader reader)
        {

            EventID = reader.GetValue<int>("EventID");
            EventName = reader.GetValue<string>("EventName");
            EventDate = reader.GetValue<DateTime>("EventDate");
            EventTime = reader.GetValue<string>("EventTime");
            Venue = reader.GetValue<string>("Venue");
            Details = reader.GetValue<string>("Details");
            AgeGroup = reader.GetValue<string>("AgeGroup");
            Incharge = reader.GetValue<string>("Incharge");
            EventImage = reader.GetValue<byte[]>("EventImage");
            PrizeDetails = reader.GetValue<string>("PrizeDetails");
            Contact = reader.GetValue<string>("Contact");
        }
    }

}
