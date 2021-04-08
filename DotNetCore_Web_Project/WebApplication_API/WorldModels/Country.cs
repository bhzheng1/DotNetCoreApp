using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_API.WorldModels
{
    [Table("country")]
    public class Country
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
    }
}