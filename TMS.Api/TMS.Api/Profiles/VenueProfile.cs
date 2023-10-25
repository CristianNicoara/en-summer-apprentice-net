using AutoMapper;
using TMS.Api.Models;
using TMS.Api.Models.DTOs;

namespace TMS.Api.Profiles
{
    public class VenueProfile : Profile
    {
        public VenueProfile()
        {
            CreateMap<Venue, VenueDTO>().ReverseMap();
        }
    }
}
