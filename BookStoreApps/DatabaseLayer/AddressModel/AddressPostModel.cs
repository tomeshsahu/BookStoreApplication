﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.AddressModel
{
    public class AddressPostModel
    {
        public int AddressId { get; set; }
       
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int TypeId { get; set; } // table for home ,office, other.
    }
}
