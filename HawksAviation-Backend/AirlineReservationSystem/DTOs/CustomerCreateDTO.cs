namespace AirlineReservationSystem.DTOs
{
    #region CustomerCreateDTO
    public class CustomerCreateDTO
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

        int _age;
        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        string? _gender;
        public string? Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        string? _emailId;
        public string? EmailId
        {
            get { return _emailId; }
            set { _emailId = value; }
        }

        string? _mobileNumber;
        public string? MobileNumber
        {
            get { return _mobileNumber; }
            set { _mobileNumber = value; }
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
    }
    #endregion
}
