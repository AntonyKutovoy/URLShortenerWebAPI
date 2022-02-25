using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URLShortener_WebAPI.Model;
using URLShortener_WebAPI.Services;

namespace URLShortener_WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly UrlService _urlService;
        private readonly IMapper _mapper;

        public UrlController(UrlService urlService, IMapper mapper)
        {
            _urlService = urlService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public ActionResult<List<UrlViewModel>> GetAll()
        {
            var urls = _urlService.GetAllUrls();
            return _mapper.Map<List<UrlViewModel>>(urls);
        }

        [HttpPost("ToShorten")]
        public IActionResult Shorten(string url)
        {
            if (url == null)
                return BadRequest("Data not filled!");
            if (!Uri.TryCreate(url, UriKind.Absolute, out var result))
                return BadRequest("URL contains an error!");
            _urlService.ShortenAndSave(url);
            return Ok();
        }
    }
}
