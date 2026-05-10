namespace api.DTOs.Customers;

public class GetCustomerDto
{
    public int Id { get; set; }
    public string? StoreName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? ContactPerson { get; set; }
    public AddressDto? DeliveryAddress { get; set; }
    public AddressDto? InvoiceAddress { get; set; }
}
