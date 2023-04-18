using Frontend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Frontend.Controllers
{
    public class ContactsController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7114/api");
        private readonly HttpClient _client;

        public ContactsController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ContactsViewModel viewModel)
        {
            string data = JsonConvert.SerializeObject(viewModel);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Contacts/Post?key=755d128a-d2ae-43f9-a521-41712709f1b5", content).Result;

            if(response.IsSuccessStatusCode) 
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
