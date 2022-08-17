using System.ComponentModel.DataAnnotations;

namespace AirlineReservationSystem.Models
{
    public class ExceptionLog
    {

        [Key]
        public int Id { get; set; }
        public DateTime DataTime { get; set; }
        public string ErrorDescription { get; set; }
        public string Data { get; set; }
        public string StackTrace { get; set; }
    }
}
