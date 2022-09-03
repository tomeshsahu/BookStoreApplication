using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.WishListModel
{
    public class WishListResponseModel
    {
        public int WishListId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }


   
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public double ActualPrice { get; set; }
        public double DiscountedPrice { get; set; }
        public string Description { get; set; }
        public string BookImage { get; set; }
        public int BookQuantity { get; set; }
    }
}
