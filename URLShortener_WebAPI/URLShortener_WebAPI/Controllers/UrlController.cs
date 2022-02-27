using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UrlController(UrlService urlService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _urlService = urlService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        //get all urls
        [HttpGet("/getall")]
        public ActionResult<List<UrlViewModel>> GetAll()
        {
            return _mapper.Map<List<UrlViewModel>>(_urlService.GetAllUrls());
        }

        //save original url to shorten
        [HttpPost("/saveurl")]
        public ActionResult Shorten(string originalUrl)
        {
            if (originalUrl == null)
                return BadRequest("URL cannot be empty.");
            if (!Uri.TryCreate(originalUrl, UriKind.Absolute, out var result))
                return BadRequest("Could not understand URL.");
            _urlService.Save($"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/", originalUrl);
            return Ok();
        }

        //get original url from shorten
        [HttpGet("/getoriginal")]
        public ActionResult<UrlViewModel> Original(string shortenedUrl)
        {
            if (shortenedUrl == null)
                return BadRequest("URL cannot be empty.");
            if (!Uri.TryCreate(shortenedUrl, UriKind.Absolute, out var result))
                return BadRequest("Could not understand URL.");
            _urlService.UpdateViewCount(shortenedUrl);
            return _mapper.Map<UrlViewModel>(_urlService.GetOriginal(shortenedUrl));
            //return Redirect(_urlService.GetOriginal(shortenedUrl).Original);
        }
    }
}
