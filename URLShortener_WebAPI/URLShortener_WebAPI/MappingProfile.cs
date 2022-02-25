using AutoMapper;
using URLShortener_WebAPI.Model;

namespace URLShortener_WebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UrlDto, UrlViewModel>().ReverseMap();
        }
    }
}
