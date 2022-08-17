using System.ComponentModel.DataAnnotations;

namespace AirlineReservationSystem.Models
{
    #region Users class properties
    public class Users
    {
        string? _username;
        [Key]
        public string? Username
        {
            get { return _username; }
            set { _username = value; }
        }

        string? _firstName;
        public string? FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        string? _lastName;
        public string? LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        string? _emailId;
        public string? EmailId
        {
            get { return _emailId; }
            set { _emailId = value; }
        }

        string? _password;
        public string? Password
        {
            get { return _password; }
            set { _password = value; }
        }

        int _userId;
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        string? _role;
        public string? Role
        {
            get { return _role; }
            set { _role = value; }
        }
    }
    #endregion
}
