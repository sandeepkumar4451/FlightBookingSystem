#region Using Namespaces
using System.ComponentModel.DataAnnotations;
#endregion

namespace AirlineReservationSystem.Models
{
    #region Login class properties
    public class Login
    {
        
        string? _username;
        [Key]
        public string? Username 
        {   
            get { return _username; } 
            set { _username = value; } 
        }

        string? _password;
        public string? Password
        {
            get { return _password; }
            set { _password = value; }
        }
    }
    #endregion
}
