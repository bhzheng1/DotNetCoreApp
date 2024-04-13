using System.ComponentModel.DataAnnotations;

namespace ClassLibrary_DataAccess.Entities;
public class Company
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Address { get; set; }
}