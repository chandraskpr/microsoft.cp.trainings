using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenAppWebApi.DataObjects.interfaces
{
    public interface IDbOperations
    {
        List<ProductData> GetAll();

        void Update(ProductData product);

        void Delete(ProductData product);

        void Insert(ProductData product);
    }
}
