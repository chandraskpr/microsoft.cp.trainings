using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EProductPortal.Domain;
using EProductPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace EProductPortal.Controllers
{
    public class ProductController : Controller
    {
        private static string productApiUrl;
        public ProductController(IConfiguration configuration)
        {
            productApiUrl = configuration.GetValue<string>("WebAPIBaseUrl");
        }
        public IActionResult Index()
        {
            ViewBag.IsEditMode = false;

            var getEditProductName = TempData["EditProductBy"];

            if (getEditProductName != null && !string.IsNullOrEmpty(getEditProductName.ToString()))
            {
                ViewBag.IsEditMode = true;
                CreateProductViewModel product = CallGetApiAsync<CreateProductViewModel>("/" + getEditProductName).Result;
                ViewData.Add("Product", product);
            }

            return View();
        }

        [HttpPost]
        public IActionResult CreateNewProduct([FromForm] CreateProductViewModel product)
        {
            ProductDomainModel domainModel = new ProductDomainModel()
            {
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                AvaliableStores = product.AvaliableStores,
                IsAvaliableOnline = product.IsAvaliableOnline
            };

            bool isCallSuccessfull = CallPostApiAsync<ProductDomainModel>(domainModel).Result;

            if (isCallSuccessfull)
            {
                return Content("Product Created Successfully !!!");
            }
            else
            {
                return Content("Product Creation Failed !!!");
            }

        }

        [HttpPost]
        public IActionResult EditProduct([FromForm] CreateProductViewModel product)
        {
            ProductDomainModel domainModel = new ProductDomainModel()
            {
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                AvaliableStores = product.AvaliableStores,
                IsAvaliableOnline = product.IsAvaliableOnline
            };

            bool isCallSuccessfull = CallPutApiAsync<ProductDomainModel>(domainModel, "/" + product.Name).Result;

            if (isCallSuccessfull)
            {
                return Content("Product Updated Successfully !!!");
            }
            else
            {
                return Content("Product Updation Failed !!!");
            }

        }

        public IActionResult DeleteProduct([FromQuery] string name)
        {
            var isCallSuccessfull = CallDeleteApiAsync("/" + name).Result;


            if (isCallSuccessfull)
            {
                return Content("Product Deleted Successfully !!!");
            }
            else
            {
                return Content("Product Deletion Failed !!!");
            }
        }

        public IActionResult ShowAllProducts()
        {
            ProductCollectionViewModel pCollection = new ProductCollectionViewModel();
            List<ProductDomainModel> data = CallGetApiAsync<List<ProductDomainModel>>("").Result;
            foreach (var product in data)
            {
                pCollection.Rows.Add(new ProductRowViewModel()
                {
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity
                });
            }

            return View("AllProducts", pCollection);
        }

        public IActionResult EditProduct([FromQuery]string name)
        {
           TempData.Add("EditProductBy", name);
            
            return RedirectToAction("Index");
        }

        private async Task<bool> CallPostApiAsync<T>(T data) where T : class
        {
            bool callIsSuccessfull = true;

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                string endpoint = productApiUrl;

                using (var Response = await client.PostAsync(endpoint, content))
                {
                    if (Response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        callIsSuccessfull = false;
                    }
                }
            }

            return callIsSuccessfull;
        }

        private async Task<bool> CallPutApiAsync<T>(T data, string extensionEndPoint) where T : class
        {
            bool callIsSuccessfull = true;

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                string endpoint = productApiUrl + extensionEndPoint;

                using (var Response = await client.PutAsync(endpoint, content))
                {
                    if (Response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        callIsSuccessfull = false;
                    }
                }
            }

            return callIsSuccessfull;
        }

        private async Task<T> CallGetApiAsync<T>(string apiName) where T : class
        {
            T returnableObject = null;

            using (HttpClient client = new HttpClient())
            {
                string endpoint = productApiUrl + apiName;

                using (var Response = await client.GetAsync(endpoint))
                {

                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var reponseContent = await Response.Content.ReadAsStringAsync();

                        returnableObject = JsonConvert.DeserializeObject<T>(reponseContent);
                    }
                }
            }

            return returnableObject;
        }

        private async Task<bool> CallDeleteApiAsync(string extensionEndPoint)
        {
            bool callIsSuccessfull = true;

            using (HttpClient client = new HttpClient())
            {                
                string endpoint = productApiUrl + extensionEndPoint;

                using (var Response = await client.DeleteAsync(endpoint))
                {
                    if (Response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        callIsSuccessfull = false;
                    }
                }
            }

            return callIsSuccessfull;
        }
    }
}
