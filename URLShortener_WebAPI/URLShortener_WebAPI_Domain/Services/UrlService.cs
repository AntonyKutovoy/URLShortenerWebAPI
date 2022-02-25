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
                    VisitCount = u.VisitCount
                })
                .ToList();
            return urls;
        }
    }
}
