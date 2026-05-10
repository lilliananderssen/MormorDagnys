using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Customers;

public class PostCustomerDto
{
    [Required]
    public string StoreName { get; set; } = string.Empty;
    [Required]
    public string Phone { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string ContactPerson { get; set; } = string.Empty;
    [Required]
    public AddressDto DeliveryAddress { get; set; } = new();
    [Required]
    public AddressDto InvoiceAddress { get; set; } = new();
}
