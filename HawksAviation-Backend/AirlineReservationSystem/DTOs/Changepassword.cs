namespace AirlineReservationSystem.DTOs
{
    #region ChangePasswordDTO
    public class Changepassword
    {
        string? _username;
        public string? Username
        {
            get { return _username; }
            set { _username = value; }
        }

        string? _oldpassword;
        public string? OldPassword
        {
            get { return _oldpassword; }
            set { _oldpassword = value; }
        }

        string? _newpassword;
        public string? NewPassword
        {
            get { return _newpassword; }
            set { _newpassword = value; }
        }
    }
    #endregion
}
