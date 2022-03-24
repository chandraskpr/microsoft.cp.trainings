using OpenAppWebApi.DataObjects.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenAppWebApi.DataObjects
{
    public class ProductTable : IDbOperations
    {
        // MILK
        // ORANGES
        // GRAPES
        public List<ProductData> products;

        public ProductTable()
        {
            products = new List<ProductData>();
            this.Insert(new ProductData() { Name = "Milk", Quantity = 200 });
            this.Insert(new ProductData() { Name = "Oranges", Quantity = 300 });
            this.Insert(new ProductData() { Name = "Grapes", Quantity = 150 });
        }

        public void Insert(ProductData product)
        {
            this.products.Add(product);
        }

        public void Delete(ProductData product)
        {
            this.products.Remove(product);
        }

        public void Update(ProductData product)
        {
            // LINQ ( Language Integrated Query ) -- C# 
            // SELECT TOP 1 * FROM PRODUCT WHERE PRODUCTNAME = "RRRR" ( SQL )
            ProductData editableProduct = this.products.FirstOrDefault(x => x.Name == product.Name);

            // SELECT * FROM PRODUCT WHERE PRODUCTNAME = "RRRR" ( SQL )
            //IEnumerable<ProductData> products = this.products.Where(x => x.Name == product.Name);

            if (editableProduct != null)
            {
                editableProduct.Quantity = product.Quantity;
                editableProduct.IsAvaliableOnline = product.IsAvaliableOnline;
                editableProduct.Price = product.Price;
                editableProduct.AvaliableStores = product.AvaliableStores;
            }
        }


        public List<ProductData> GetAll()
        {
            return this.products;
        }
        
    }
}
