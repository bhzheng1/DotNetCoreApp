using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace example.Model
{
    [Table("_SchemaVersions")]
    public partial class SchemaVersion
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string ScriptName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Applied { get; set; }
    }
}
