using AutoMapper;
using E_Commerce.Application.DTOS.Orders;
using E_Commerce.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.MappingProfile.Oeders
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderAddressDto, OrderAddress>().ReverseMap();
            CreateMap<Order, OrderToReturn>()
                     .ForMember(D => D.DeliveryMethod,
                     opt => opt.MapFrom(S => S.DeliveryMethod.ShortName))
                     .ForMember(D => D.Status,
                     opt => opt.MapFrom(S => S.Status.ToString()))
                     .ForMember(D => D.DeliveryMethodCost,
                     opt => opt.MapFrom(S => S.DeliveryMethod.Cost))
                     .ForMember(D => D.Total,
                     opt => opt.MapFrom(S => S.GetTotal()));


            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(D => D.ProductId,
                     opt => opt.MapFrom(S => S.Product.ProductId))

                .ForMember(D => D.ProductName,
                     opt => opt.MapFrom(S => S.Product.ProductName))

                .ForMember(D => D.PictureUrl,
                     opt => opt.MapFrom(S => S.Product.PictureUrl))
                ;

            CreateMap<DeliveryMethod, DeliveryMethodDto>();
        }
    }
}
