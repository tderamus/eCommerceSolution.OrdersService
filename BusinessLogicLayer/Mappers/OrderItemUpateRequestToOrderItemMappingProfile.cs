﻿
using AutoMapper;
using eCommerce.OrdersMicroservice.BusinessLogicLayer.DTO;
using eCommerce.OrdersMicroservice.DataAccessLayer.Entities;
using MongoDB.Driver.Core.Authentication;

namespace eCommerce.OrdersMicroservice.BusinessLogicLayer.Mappers;

public class OrderItemUpdateRequestToOrderItemMappingProfile : Profile
{
    public OrderItemUpdateRequestToOrderItemMappingProfile()
    {
        CreateMap<OrderItemAddRequest, OrderItem>()
        .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductID))
        .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
        .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
        .ForMember(dest => dest.TotalPrice, opt => opt.Ignore())
        .ForMember(dest => dest._id, opt => opt.Ignore());
    }

}