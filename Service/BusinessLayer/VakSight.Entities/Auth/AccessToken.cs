namespace VakSight.Entities.Auth
{
    public sealed class AccessToken
    {
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
    }
}
