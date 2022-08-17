namespace AirlineReservationSystem.DTOs
{
    #region AdminCreateDTO
    public class AdminCreateDTO
    {
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

        string? _username;
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

        string? _role;
        public string? Role
        {
            get { return _role; }
            set { _role = value; }
        }
    }
    #endregion
}
