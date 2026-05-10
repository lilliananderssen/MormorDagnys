using api.DTOs.Customers;
using core.Entities;

namespace api.Extensions;

public static class CustomerMappingExtensions
{
    public static CustomerDetailDto ToDetailDTO(this Customer customer)
    {
        return new CustomerDetailDto
        {
            Id = customer.Id,
            StoreName = customer.StoreName,
            Phone = customer.Phone,
            Email = customer.Email,
            ContactPerson = customer.ContactPerson,
            DeliveryAddress = new AddressDto
            {
                Street = customer.DeliveryAddress.Street,
                PostalCode = customer.DeliveryAddress.PostalCode,
                City = customer.DeliveryAddress.City
            },
            InvoiceAddress = new AddressDto
            {
                Street = customer.InvoiceAddress.Street,
                PostalCode = customer.InvoiceAddress.PostalCode,
                City = customer.InvoiceAddress.City
            },
            Orders = customer.Orders.Select(o => new CustomerOrderSummaryDto
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                OrderDate = o.OrderDate,
                TotalAmount = o.OrderItems.Sum(i => i.Price * i.Quantity)
            }).ToList()
        };
    }
}

public class CustomerDetailDto : GetCustomerDto
{
    public List<CustomerOrderSummaryDto> Orders { get; set; } = [];
}

public class CustomerOrderSummaryDto
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
}
