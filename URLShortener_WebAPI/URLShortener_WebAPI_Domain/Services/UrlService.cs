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
                    Id = u.Id,
                    Original = u.Original,
                    Shortened = u.Shortened,
                    ViewCount = u.ViewCount
                })
                .ToList();
            return urls;
        }

        public void ShortenAndSave(string url)
        {
            var newUrl = new Url()
            {
                Original = url,
                Shortened = "bit.ly/" + Guid.NewGuid().ToString().Split('-')[0],
                ViewCount = 0,
            };
            _urlRepository.Save(newUrl);
        }
    }
}
