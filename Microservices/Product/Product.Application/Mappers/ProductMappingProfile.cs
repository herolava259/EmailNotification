using AutoMapper;
using Product.Application.Commands;
using Product.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductEntity = Product.Core.Entities.Product;
namespace Product.Application.Mappers
{
    internal class ProductMappingProfile: Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductEntity, ProductResponse>().ReverseMap();

            CreateMap<UpdateProductCommand, ProductEntity>();
        }
    }
}
