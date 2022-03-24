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
    public class DashboardController : Controller
    {
        private static string productApiUrl;
        public DashboardController(IConfiguration configuration)
        {
            productApiUrl = configuration.GetValue<string>("WebAPIBaseUrl");
        }
        public IActionResult Index()
        {
            List<ProductDomainModel> data = CallGetApiAsync<List<ProductDomainModel>>("").Result;

            ProductQuantityCollectionViewModel productQuantityCollectionViewModel = new ProductQuantityCollectionViewModel();
            foreach (ProductDomainModel item in data)
            {                
                ProductQuantityViewModel quantityViewModel = new ProductQuantityViewModel()
                {
                    Name = item.Name,
                    Quantity = item.Quantity
                };
                productQuantityCollectionViewModel.collection.Add(quantityViewModel);
            }

            ViewData["ProductQuantity"] = productQuantityCollectionViewModel;

            #region COMMENTED CODE
            //ViewData["ProductQuantity"] = new ProductQuantityCollectionViewModel()
            //{
            //    collection = new List<ProductQuantityViewModel>
            //    {
            //        new ProductQuantityViewModel() { Name = "Milk", Quantity = 200 },
            //        new ProductQuantityViewModel() { Name = "Oranges", Quantity = 150 },
            //        new ProductQuantityViewModel() { Name = "Carrots", Quantity = 30 },
            //        new ProductQuantityViewModel() { Name = "Grapes", Quantity = 225 },
            //        new ProductQuantityViewModel() { Name = "Apples", Quantity = 75 }
            //    }
            //};
            #endregion

            return View();
        }
       
        private async Task<T> CallGetApiAsync<T>(string apiName) where T : class
        {
            T returnableObject = null;

            using (HttpClient client = new HttpClient())
            {
                ///Serializing Converting C# Objects to Other Type (JSON,XML,..)
                ///DeSerializing Converting Other Type (JSON,XML,..) to C# Objects
                //StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                string endpoint = productApiUrl + apiName;

                using (var Response = await client.GetAsync(endpoint))
                {
//                    Response.IsSuccessStatusCode
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        // ReadAsByteArrayAsync
                        //ReadAsStreamAsync
                        var reponseContent = await Response.Content.ReadAsStringAsync();

                        returnableObject = JsonConvert.DeserializeObject<T>(reponseContent);
                    }
                }
            }

            return returnableObject;
        }
    }
}
