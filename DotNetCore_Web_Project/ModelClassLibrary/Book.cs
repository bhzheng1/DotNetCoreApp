using System;
using System.ComponentModel.DataAnnotations;
namespace ModelClassLibrary
{
    //使用attribute检查数据合法性
    public class Book
    {
        [Required, Key]
        public int ISBN { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        [Required, MaxLength(1000)]
        public string Description { get; set; }

        [Required, Range(0, 999.99)]
        public decimal Price { get; set; }

        [Url]
        public bool SamplePage { get; set; }

        [EmailAddress]
        public string AuthorEmail { get; set; }

        [Phone]
        public string AuthorPhone { get; set; }
    }
}
