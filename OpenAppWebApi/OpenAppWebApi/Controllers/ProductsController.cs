using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenAppWebApi.DataObjects.interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenAppWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IDbOperations productOperations;
        public ProductsController(IDbOperations _productOperations)
        {
            this.productOperations = _productOperations;
            //this.productOperations.Insert(new ProductData() { Name = "Milk", Quantity = 200 });
            //this.productOperations.Insert(new ProductData() { Name = "Oranges", Quantity = 300 });
            //this.productOperations.Insert(new ProductData() { Name = "Grapes", Quantity = 150 });

        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<ProductData> Get()
        {
            return this.productOperations.GetAll();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{name}")]
        public ProductData Get(string name)
        {
            return this.productOperations.GetAll().FirstOrDefault(x => x.Name == name); 
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] ProductData value)
        {
            this.productOperations.Insert(value);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(string name, [FromBody] ProductData value)
        {
            this.productOperations.Update(value);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{name}")]
        public void Delete(string name)
        {
            var product = this.productOperations.GetAll().FirstOrDefault(x => x.Name == name);

            this.productOperations.Delete(product);
        }
    }
}
