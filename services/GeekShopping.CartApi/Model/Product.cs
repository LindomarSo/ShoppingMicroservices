﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CartApi.Model;

[Table("product")]
public class Product
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column("id")]
    public long Id { get; set; }

    [Column("name")]
    [Required]
    [StringLength(150)]
    public string Name { get; set; } = null!;

    [Column("price")]
    [Required]
    [Range(1, 10000)]
    public decimal Price { get; set; }

    [Column("description")]
    [StringLength(500)]
    public string? Description { get; set; }


    [Column("category_name")]
    [StringLength(150)]
    public string? CategoryName { get; set; }


    [Column("image_url")]
    [StringLength(300)]
    public string? ImageUrl { get; set; }
}
