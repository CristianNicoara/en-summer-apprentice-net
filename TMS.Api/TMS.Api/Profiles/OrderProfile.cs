using AutoMapper;
using TMS.Api.Models;
using TMS.Api.Models.DTOs;

namespace TMS.Api.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDTO>().ReverseMap();
        }
    }
}
