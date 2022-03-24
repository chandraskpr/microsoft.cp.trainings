using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EProductPortal.Models
{
    public class ProductQuantityCollectionViewModel
    {
        public List<ProductQuantityViewModel> collection { get; set; }

        public ProductQuantityCollectionViewModel()
        {
            collection = new List<ProductQuantityViewModel>();
        }
    }
}
