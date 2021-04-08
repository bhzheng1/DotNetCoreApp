using AutoMapper;
using ModelClassLibrary;
using WebApplication_API.SakilaModels;

namespace WebApplication_API.MappingConfiguration
{
    public class AddressProfile:Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressModel>();
        }
    }

    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryModel>();
        }

    }
}
