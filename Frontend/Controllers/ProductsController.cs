using Frontend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Frontend.Controllers
{
    public class ProductsController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7114/api");
        private readonly HttpClient _client;

        public ProductsController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<ProductsViewModel> productslist = new List<ProductsViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/products/Get?key=755d128a-d2ae-43f9-a521-41712709f1b5").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                productslist = JsonConvert.DeserializeObject<List<ProductsViewModel>>(data);
            }


            return View(productslist);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id) 
        { 
            ProductsViewModel model = new ProductsViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/products/Get/{id}?key=755d128a-d2ae-43f9-a521-41712709f1b5").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<ProductsViewModel>(data);
            }
            return View(model);
        }
    }
}
