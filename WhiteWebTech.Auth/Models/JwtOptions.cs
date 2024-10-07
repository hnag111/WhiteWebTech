namespace WhiteWebTech.Auth.Models
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public string Issues { get; set; }
        public string Audience { get; set; }
    }
}
