namespace CavipetrolTestBack.API.Models
{
    public class ApiConfig
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public int SessionDurationMinutes { get; set; }
        public string Audience { get; set; }
    }
}
