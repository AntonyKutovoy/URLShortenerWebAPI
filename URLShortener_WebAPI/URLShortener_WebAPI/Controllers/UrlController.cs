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
    [Route("api/[controller]")]
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

        [HttpGet]
        public ActionResult<List<UrlViewModel>> GetAll()
        {
            var urls = _urlService.GetAllUrls();
            return _mapper.Map<List<UrlViewModel>>(urls);
        }
    }
}
