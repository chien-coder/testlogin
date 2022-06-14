namespace UserAPI_NetCore6.DataConfig
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuaer { get; set; }

        public string Audience { get; set; }
        public string Subject { get; set; }
    }
}
