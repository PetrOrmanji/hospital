namespace Hospital.Options
{
    public class JwtOptions
    {
        public string ClientSecret { get; set; }
        public int ExpiresHours { get; set; }
    }
}
