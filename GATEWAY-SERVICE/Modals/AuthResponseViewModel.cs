using Azure.Core;

namespace GATEWAY_SERVICE.Modals
{
    public class AuthResponseViewModel
    {
        public UserViewModel User { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public AuthResponseViewModel()
        {
        }

        public AuthResponseViewModel(UserViewModel user,string accessToken,string refreshToken)
        {
            User = user;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
        
    }
}
