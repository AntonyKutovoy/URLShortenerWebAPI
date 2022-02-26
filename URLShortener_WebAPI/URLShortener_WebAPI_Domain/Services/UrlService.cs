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
                    Shortened = GetShortenedUrl(u.Id),
                    Original = u.Original,
                    ViewCount = u.ViewCount
                })
                .ToList();
            if (urls != null)
            {
                return urls;
            }
            else
                throw new ArgumentNullException();
        }

        public void Save(string originalUrl)
        {
            var url = _urlRepository.TryGetByOriginal(originalUrl);
            if (url == null)
            {
                var idGenerator = new Random();
                var newUrl = new Url()
                {
                    Id = idGenerator.Next(100000, 999999),
                    Original = originalUrl,
                    ViewCount = 0,
                };
                _urlRepository.Save(newUrl);
            }
            else
                throw new ArgumentNullException();
        }

        public string GetShortenedUrl(int id)
        {
            return WebEncoders.Base64UrlEncode(BitConverter.GetBytes(id));
        }
    }
}
