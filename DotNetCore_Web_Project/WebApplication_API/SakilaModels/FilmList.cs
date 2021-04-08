using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_API.SakilaModels
{
    public partial class FilmList
    {
        [Column("FID")]
        public int? Fid { get; set; }
        [Column("title")]
        [StringLength(255)]
        public string Title { get; set; }
        [Column("description", TypeName = "text")]
        public string Description { get; set; }
        [Required]
        [Column("category")]
        [StringLength(25)]
        public string Category { get; set; }
        [Column("price", TypeName = "decimal(4, 2)")]
        public decimal? Price { get; set; }
        [Column("length")]
        public short? Length { get; set; }
        [Column("rating")]
        [StringLength(10)]
        public string Rating { get; set; }
        [Required]
        [Column("actors")]
        [StringLength(91)]
        public string Actors { get; set; }
    }
}
