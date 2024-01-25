namespace USER_SERVICE.Modals
{
    public class AuthResponseViewModel
    {
        public UserViewModel User { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
