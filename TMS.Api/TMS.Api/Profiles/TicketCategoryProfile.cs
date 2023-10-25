using AutoMapper;
using TMS.Api.Models;
using TMS.Api.Models.DTOs;

namespace TMS.Api.Profiles
{
    public class TicketCategoryProfile : Profile
    {
        public TicketCategoryProfile()
        {
            CreateMap<TicketCategory, TicketCategoryDTO>().ReverseMap();
        }
    }
}
