namespace VakSight.Entities.Auth
{
    public class LoginResult
    {
        public AccessToken AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
