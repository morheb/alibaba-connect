using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using alibaba.Data;
using alibaba.Services.Models;

namespace alibaba.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DbUser, User>();

            CreateMap<User, DbUser>();

            CreateMap<DbTalabak, Talabak>();

            CreateMap<Talabak, DbTalabak>();

            CreateMap<DbUserAddress, UserAddress>();

            CreateMap<UserAddress, DbUserAddress>();

            CreateMap<DbTalabakStatus, TalabakStatus>();

            CreateMap<TalabakStatus, DbTalabakStatus>();

            CreateMap<DbTalabakCriteria, TalabakCriteria>();

            CreateMap<TalabakCriteria, DbTalabakCriteria>();
         
            CreateMap<DbResponse, Response>();

            CreateMap<Response, DbResponse>();
            CreateMap<DbUser, User>();

            CreateMap<User, DbUser>();

            CreateMap<DbUserStatus, UserStatus>();

            CreateMap<UserStatus, DbUserStatus>();

            CreateMap<DbRestaurant, Restaurant>();

            CreateMap<Restaurant, DbRestaurant>();

            CreateMap<Banner, DbBanner>();
            CreateMap<DbBanner, Banner>();

            CreateMap<RestaurantCriteria, DbRestaurantCriteria>();
            CreateMap<DbRestaurantCriteria, RestaurantCriteria>();
            
            CreateMap<UserCriteria, DbUserCriteria>();
            CreateMap<DbUserCriteria, UserCriteria>();

            CreateMap<AddonOrder, DbAddonOrder>();
            CreateMap<DbAddonOrder, AddonOrder>();

            CreateMap<ProductCriteria, DbProductCriteria>();
            CreateMap<DbProductCriteria, ProductCriteria>();

            CreateMap<Rating, DbRating>();
            CreateMap<DbRating, Rating>();

            CreateMap<OrderCriteria, DbOrderCriteria>();
            CreateMap<DbOrderCriteria, OrderCriteria>();
             
            CreateMap<DbRestaurantStatus,RestaurantStatus>();

            CreateMap<RestaurantStatus, DbRestaurantStatus>();

             CreateMap<DbProductStatus,ProductStatus>();

            CreateMap<ProductStatus, DbProductStatus>();
             CreateMap<DbRestaurantStatus,RestaurantStatus>();

            CreateMap<RestaurantStatus, DbRestaurantStatus>();
             CreateMap<DbOrderStatus,OrderStatus>();

            CreateMap<OrderStatus, DbOrderStatus>();
           
        
            CreateMap<DbProduct, Product>();

            CreateMap<Product, DbProduct>();
        
            CreateMap<DbCommentCriteria, CommentCriteria>();

            CreateMap<CommentCriteria, DbCommentCriteria>();
            
            CreateMap<DbAddon, Addon>();

            CreateMap<Addon, DbAddon>();
        
            CreateMap<DbAddonCriteria, AddonCriteria>();

            CreateMap<AddonCriteria, DbAddonCriteria>();
        
            CreateMap<DbComment, Comment>();

            CreateMap<Comment, DbComment>();
            CreateMap<DbUserActiveStatus, UserActiveStatus>();

            CreateMap<UserActiveStatus, DbUserActiveStatus>();
         
            CreateMap<DbProductOfferRequest, ProductOfferRequest>();

            CreateMap<ProductOfferRequest, DbProductOfferRequest>();
           
        
            CreateMap<DbOrder, Order>();

            CreateMap<Order, DbOrder>();
            CreateMap<ProductOrder, DbProductOrder>();

            CreateMap<DbProductOrder, ProductOrder>();
           
        
            CreateMap<DbRestaurantStatus, RestaurantStatus>();

            CreateMap<RestaurantStatus, DbRestaurantStatus>();

    
            
            CreateMap<DbCategory, Category>();

            CreateMap<Category, DbCategory>();
            
            
            CreateMap<DbBrand, Brand>();

            CreateMap<Brand, DbBrand>();

    

        }

   
    }
}
