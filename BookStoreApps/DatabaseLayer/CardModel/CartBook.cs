using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.CardModel
{
    public class CartBook
    {
        public int CartId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int BookQuantity { get; set; }

    
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public double ActualPrice { get; set; }
        public double DiscountPrice { get; set; }
        public string Description { get; set; }
        public string BookImage { get; set; }
    }
}
