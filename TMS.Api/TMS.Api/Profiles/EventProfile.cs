using AutoMapper;
using TMS.Api.Models;
using TMS.Api.Models.DTOs;

namespace TMS.Api.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDTO>().ReverseMap();
            CreateMap<Event, EventPatchDTO>().ReverseMap();
        }
    }
}
