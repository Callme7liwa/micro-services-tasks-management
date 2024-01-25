namespace WebApplic.Modal
{
    public class UserCred
    {
        public string username { get; set; }
        public string email { get; set; }
        public string id {  get; set; }
        public string password { get; set; }

        public List<CustomClaims> userClaims = new List<CustomClaims>();

        public class CustomClaims
        {
            public String role;
            public String value;
        }
    }
}
