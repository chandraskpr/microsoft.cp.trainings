using System;
using System.Collections.Generic;

#nullable disable

namespace OpenAppWebApi.EBusinessDb
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public bool IsAvaliable { get; set; }
        public string AvaliableStores { get; set; }
    }
}
