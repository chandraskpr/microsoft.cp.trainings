using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EProductPortal.Models
{
    public class ProductCollectionViewModel
    {
        public List<ProductRowViewModel> Rows { get; set; }

        public ProductCollectionViewModel()
        {
            Rows = new List<ProductRowViewModel>();
        }
    }
}
