using System.ComponentModel.DataAnnotations;
namespace ClassLibrary_DataAccess.Entities;
public class Product
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
}