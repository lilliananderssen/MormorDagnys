using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Customers;

public class PatchContactPersonDto
{
    [Required]
    public string ContactPerson { get; set; } = string.Empty;
}
