namespace CompanySystem.BLL
{

    public class JwtSettings
    {
        public string Issuer           { get; set; } = string.Empty;
        public string Audience         { get; set; } = string.Empty;
        public int    DurationInMinutes { get; set; } = 60;

        public string SecretKey { get; set; } = string.Empty;
    }
}
