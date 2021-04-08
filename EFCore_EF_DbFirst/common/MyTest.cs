using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace example.common
{
    [Table("MyTest")]
    public partial class MyTest
    {
        [Key]
        [Column("myKey")]
        public int MyKey { get; set; }
        [Column("myValue")]
        public int? MyValue { get; set; }
        [Required]
        [Column("RV")]
        public byte[] Rv { get; set; }
    }
}
