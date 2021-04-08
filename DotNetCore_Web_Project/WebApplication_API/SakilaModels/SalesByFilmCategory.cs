using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_API.SakilaModels
{
    public partial class SalesByFilmCategory
    {
        [Required]
        [Column("category")]
        [StringLength(25)]
        public string Category { get; set; }
        [Column("total_sales", TypeName = "decimal(38, 2)")]
        public decimal? TotalSales { get; set; }
    }
}
