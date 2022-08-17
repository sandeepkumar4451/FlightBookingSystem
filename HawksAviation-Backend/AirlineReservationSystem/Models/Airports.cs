#region Using Namespaces
using System.ComponentModel.DataAnnotations;
#endregion

namespace AirlineReservationSystem.Models
{
    #region Airports class properties
    public class Airports
    {
        
        string? _AID;
        [Key]
        public string AirportID
        {
            get { return _AID; }
            set { _AID = value; }
        }

        string? _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        string? _city;
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        string? _country;
        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }
    }
    #endregion
}