namespace HereWebApp.Models
{
    public interface IHereApiSettings
    {
        string BaseUrl { get; set; }

        string AppId { get; set; }

        string AppCode { get; set; }
    }

    public class HereApiSettings : IHereApiSettings
    {
        public string BaseUrl { get; set; }

        public string AppId { get; set; }

        public string AppCode { get; set; }
    }
}
