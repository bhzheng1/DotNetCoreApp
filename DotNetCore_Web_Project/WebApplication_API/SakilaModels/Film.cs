using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_API.SakilaModels
{
    [Table("film")]
    public partial class Film
    {
        public Film()
        {
            FilmActor = new HashSet<FilmActor>();
            FilmCategory = new HashSet<FilmCategory>();
            Inventory = new HashSet<Inventory>();
        }

        [Key]
        [Column("film_id")]
        public int FilmId { get; set; }
        [Required]
        [Column("title")]
        [StringLength(255)]
        public string Title { get; set; }
        [Column("description", TypeName = "text")]
        public string Description { get; set; }
        [Column("release_year")]
        [StringLength(4)]
        public string ReleaseYear { get; set; }
        [Column("language_id")]
        public byte LanguageId { get; set; }
        [Column("original_language_id")]
        public byte? OriginalLanguageId { get; set; }
        [Column("rental_duration")]
        public byte RentalDuration { get; set; }
        [Column("rental_rate", TypeName = "decimal(4, 2)")]
        public decimal RentalRate { get; set; }
        [Column("length")]
        public short? Length { get; set; }
        [Column("replacement_cost", TypeName = "decimal(5, 2)")]
        public decimal ReplacementCost { get; set; }
        [Column("rating")]
        [StringLength(10)]
        public string Rating { get; set; }
        [Column("special_features")]
        [StringLength(255)]
        public string SpecialFeatures { get; set; }
        [Column("last_update", TypeName = "datetime")]
        public DateTime LastUpdate { get; set; }

        [ForeignKey(nameof(LanguageId))]
        [InverseProperty("FilmLanguage")]
        public virtual Language Language { get; set; }
        [ForeignKey(nameof(OriginalLanguageId))]
        [InverseProperty("FilmOriginalLanguage")]
        public virtual Language OriginalLanguage { get; set; }
        [InverseProperty("Film")]
        public virtual ICollection<FilmActor> FilmActor { get; set; }
        [InverseProperty("Film")]
        public virtual ICollection<FilmCategory> FilmCategory { get; set; }
        [InverseProperty("Film")]
        public virtual ICollection<Inventory> Inventory { get; set; }
    }
}
