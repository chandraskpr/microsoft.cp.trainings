using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EProductPortal.Domain
{
    public class ProductDomainModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public bool IsAvaliableOnline { get; set; }
        public string AvaliableStores { get; set; }
        public int Quantity { get; set; }
    }
}
