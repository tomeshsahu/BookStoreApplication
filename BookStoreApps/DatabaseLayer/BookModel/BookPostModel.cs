using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseLayer.BookModel
{
    public class BookPostModel
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public double ActualPrice { get; set; }
        [Required]
        public double DiscountedPrice { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        public string BookImage { get; set; }
    }
}
