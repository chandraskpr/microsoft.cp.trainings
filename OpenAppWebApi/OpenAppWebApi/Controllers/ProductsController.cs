using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenAppWebApi.DataObjects.interfaces;
using OpenAppWebApi.EBusinessDb;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenAppWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IDbOperations productOperations;
        private EBusinessPortalContext portalDbContext;
        public ProductsController(
            IDbOperations _productOperations,
            EBusinessPortalContext _portalDbContext)
        {
            this.productOperations = _productOperations;
            this.portalDbContext = _portalDbContext;

        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return this.portalDbContext.Products.AsEnumerable();
            //this.productOperations.GetAll();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{name}")]
        public Product Get(string name)
        {
            return this.portalDbContext.Products.FirstOrDefault(x => x.Name == name);
            //this.productOperations.GetAll().FirstOrDefault(x => x.Name == name); 
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] Product value)
        {
            this.portalDbContext.Products.Add(value);
            this.portalDbContext.SaveChanges();
            //this.productOperations.Insert(value);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(string name, [FromBody] Product value)
        {
            this.portalDbContext.Products.Update(value);
            this.portalDbContext.SaveChanges();
            //this.productOperations.Update(value);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{name}")]
        public void Delete(string name)
        {
            //var product = this.productOperations.GetAll().FirstOrDefault(x => x.Name == name);
            //this.productOperations.Delete(product);

            var producttoDelete = this.portalDbContext.Products.FirstOrDefault(x => x.Name == name);
            this.portalDbContext.Products.Remove(producttoDelete);
            this.portalDbContext.SaveChanges();

        }
    }
}
