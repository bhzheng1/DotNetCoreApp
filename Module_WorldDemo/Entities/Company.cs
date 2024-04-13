using System.ComponentModel.DataAnnotations;

namespace Module_WorldDemo.Entities;
public class Company
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Address { get; set; }
}