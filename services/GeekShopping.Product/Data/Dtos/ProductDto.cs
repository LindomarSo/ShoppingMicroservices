using System.ComponentModel.DataAnnotations;

namespace GeekShopping.Product.Data.Dtos;

public class ProductDto
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório")]
    [MinLength(3, ErrorMessage = "Não deve ter menos de 3 caracteres")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Preço é obritório")]
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? CategoryName { get; set; }
    public string? ImageUrl { get; set; }
}
