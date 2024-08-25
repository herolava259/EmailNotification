using AutoMapper;
using Cart.Application.Responses;
using Cart.Core.Entities;

namespace Cart.Application.Mappers
{
     public class CartMappingProfile : Profile
    {
        public CartMappingProfile()
        {
            CreateMap<CartResponse, Core.Entities.Cart>().ReverseMap();
            CreateMap<ListItemResponse, ListItem>().ReverseMap();
        }
    }
}
