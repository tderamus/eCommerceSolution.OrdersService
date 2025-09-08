
using AutoMapper;
using eCommerce.OrdersMicroservice.BusinessLogicLayer.DTO;
using eCommerce.OrdersMicroservice.DataAccessLayer.Entities;
using MongoDB.Driver.Core.Authentication;

namespace eCommerce.OrdersMicroservice.BusinessLogicLayer.Mappers;

public class OrderUpdateRequestToOrderMappingProfile : Profile
{
    public OrderUpdateRequestToOrderMappingProfile()
    {
        CreateMap<OrderUpdateRequest, Order>()
        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserID))
        .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
        .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
        .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderID))
        .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
        .ForMember(dest => dest._id, opt => opt.Ignore());
    }

}