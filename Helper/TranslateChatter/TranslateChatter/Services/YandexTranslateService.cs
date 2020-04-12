using System.Net.Http;

namespace TranslateChatter.Services
{
    public interface ITranslateService
    {
        string Translate(string text, string lang);
    }

    public class TranslateConfiguration
    {
        public string Key { get; set; }
        public string BaseUrl { get; set; }
    }


    public class YandexTranslateService : ITranslateService
    {
        private readonly TranslateConfiguration translateConfiguration;

        public YandexTranslateService(TranslateConfiguration translateConfiguration)
        {
            this.translateConfiguration = translateConfiguration;
        }

        public string Translate(string text, string lang)
        {
            using HttpClient httpClient = new HttpClient();

            return string.Empty;
        }
    }
}
