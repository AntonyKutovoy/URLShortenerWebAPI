using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URLShortener_WebAPI.Model;
using URLShortener_WebAPI_DataAccess;

namespace URLShortener_WebAPI.Services
{
    public class UrlService
    {
        private readonly IUrlRepository _urlRepository;

        public UrlService(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public List<UrlDto> GetAllUrls()
        {
            var urls = _urlRepository.GetAll()
                .Select(u => new UrlDto
                {
                    Shortened = u.Shortened,
                    Original = u.Original,
                    ViewCount = u.ViewCount
                })
                .ToList();
            if (!(urls.Count == 0))
            {
                return urls;
            }
            else
                throw new ArgumentNullException();
        }

        public void Save(string path, string originalUrl)
        {
            var url = _urlRepository.TryGetByOriginal(originalUrl);
            if (url == null)
            {
                var generator = new Random();
                var newUrl = new Url()
                {
                    Shortened = path + GenerateUrlChunk(generator.Next(100000, 999999)),
                    Original = originalUrl,
                    ViewCount = 0,
                };
                _urlRepository.Save(newUrl);
            }
            else
                throw new ArgumentException("URL is already in the database.");
        }

        public void UpdateViewCount(string shortenedUrl)
        {
            var url = _urlRepository.TryGetByShortened(shortenedUrl);
            if (url != null)
            {
                url.ViewCount++;
                _urlRepository.UpdateViewCount(url);
            }
            else
                throw new ArgumentException("URL is not in database.");
        }

        public UrlDto GetOriginal(string shortenedUrl)
        {
            var url = _urlRepository.TryGetByShortened(shortenedUrl);
            if (url != null)
            {
                var urlDto = new UrlDto()
                {
                    Shortened = shortenedUrl,
                    Original = url.Original,
                    ViewCount = url.ViewCount
                };
                return urlDto;
            }
            else
                throw new ArgumentException("URL is not in database.");
        }

        private string GenerateUrlChunk(int generator)
        {
            return WebEncoders.Base64UrlEncode(BitConverter.GetBytes(generator));
        }
    }
}
