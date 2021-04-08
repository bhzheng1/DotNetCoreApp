using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_API.SakilaModels
{
    [Table("language")]
    public partial class Language
    {
        public Language()
        {
            FilmLanguage = new HashSet<Film>();
            FilmOriginalLanguage = new HashSet<Film>();
        }

        [Key]
        [Column("language_id")]
        public byte LanguageId { get; set; }
        [Required]
        [Column("name")]
        [StringLength(20)]
        public string Name { get; set; }
        [Column("last_update", TypeName = "datetime")]
        public DateTime LastUpdate { get; set; }

        [InverseProperty(nameof(Film.Language))]
        public virtual ICollection<Film> FilmLanguage { get; set; }
        [InverseProperty(nameof(Film.OriginalLanguage))]
        public virtual ICollection<Film> FilmOriginalLanguage { get; set; }
    }
}
