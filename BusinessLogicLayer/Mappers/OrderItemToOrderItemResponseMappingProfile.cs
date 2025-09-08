﻿
using AutoMapper;
using eCommerce.OrdersMicroservice.BusinessLogicLayer.DTO;
using eCommerce.OrdersMicroservice.DataAccessLayer.Entities;
using MongoDB.Driver.Core.Authentication;

namespace eCommerce.OrdersMicroservice.BusinessLogicLayer.Mappers;

public class OrderItemToOrderItemResponseMappingProfile : Profile
{
    public OrderItemToOrderItemResponseMappingProfile()
    {
        CreateMap<OrderItem, OrderItemResponse>()
        .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductId))
        .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
        .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
        .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice));
    }

}