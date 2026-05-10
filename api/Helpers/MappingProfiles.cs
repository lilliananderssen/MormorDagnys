using api.DTOs.BakeryProducts;
using api.DTOs.Customers;
using api.DTOs.Suppliers;
using AutoMapper;
using core.Entities;

namespace api.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<PostSupplierDto, Supplier>();
        CreateMap<Supplier, GetSupplierDto>();

        CreateMap<PostCustomerDto, Customer>()
            .ForMember(d => d.DeliveryAddress, m => m.MapFrom(s => new core.Entities.CustomerAddress
            {
                Street = s.DeliveryAddress.Street,
                PostalCode = s.DeliveryAddress.PostalCode,
                City = s.DeliveryAddress.City
            }))
            .ForMember(d => d.InvoiceAddress, m => m.MapFrom(s => new core.Entities.CustomerAddress
            {
                Street = s.InvoiceAddress.Street,
                PostalCode = s.InvoiceAddress.PostalCode,
                City = s.InvoiceAddress.City
            }));

        CreateMap<Customer, GetCustomerDto>()
            .ForMember(d => d.DeliveryAddress, m => m.MapFrom(s => new AddressDto
            {
                Street = s.DeliveryAddress.Street,
                PostalCode = s.DeliveryAddress.PostalCode,
                City = s.DeliveryAddress.City
            }))
            .ForMember(d => d.InvoiceAddress, m => m.MapFrom(s => new AddressDto
            {
                Street = s.InvoiceAddress.Street,
                PostalCode = s.InvoiceAddress.PostalCode,
                City = s.InvoiceAddress.City
            }));

        CreateMap<PostBakeryProductDto, BakeryProduct>();
        CreateMap<BakeryProduct, GetBakeryProductDto>();
    }
}
