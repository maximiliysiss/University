﻿using System.Net.Http;
using System.Threading.Tasks;

namespace TranslateChatter.Services
{
    /// <summary>
    /// Сервис для перевода
    /// </summary>
    public interface ITranslateService
    {
        /// <summary>
        /// Перевод
        /// </summary>
        /// <param name="text"></param>
        /// <param name="langTo"></param>
        /// <param name="langFrom"></param>
        /// <returns></returns>
        Task<string> Translate(string text, string langTo, string langFrom);
    }

    /// <summary>
    /// Настройка
    /// </summary>
    public class TranslateConfiguration
    {
        /// <summary>
        /// Ключ
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Базовый url
        /// </summary>
        public string BaseUrl { get; set; }
    }

    public class TranslateResult
    {
        public int Code { get; set; }
        public string Lang { get; set; }
        public string[] Text { get; set; }
    }


    public class YandexTranslateService : ITranslateService
    {
        private readonly TranslateConfiguration translateConfiguration;

        public YandexTranslateService(TranslateConfiguration translateConfiguration)
        {
            this.translateConfiguration = translateConfiguration;
        }

        public async Task<string> Translate(string text, string langTo, string langFrom)
        {
            using HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("*/*"));
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            StringContent stringContent = new StringContent($"text={text}");
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var tranlateResult = await httpClient.PostAsync($"{translateConfiguration.BaseUrl}?lang={langFrom}-{langTo}&key={translateConfiguration.Key}", stringContent);
            if (tranlateResult.IsSuccessStatusCode)
                return string.Join(" ", (await tranlateResult.Content.ReadAsAsync<TranslateResult>()).Text);
            return "";
        }
    }
}
