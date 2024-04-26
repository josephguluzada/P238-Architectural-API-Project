using BlankSolution.MVC.Models;
using BlankSolution.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace BlankSolution.MVC.Controllers
{
    public class HomeController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7266/api");
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = baseAddress;
        }

        public async Task<IActionResult> Index()
        {
            List<GenreGetListViewModel> list = new List<GenreGetListViewModel>();
            var responseMessage = await _httpClient.GetAsync(baseAddress + "/genres/getall");
            if(responseMessage.IsSuccessStatusCode)
            {
                var datas = await responseMessage.Content.ReadAsStringAsync();

                list = JsonConvert.DeserializeObject<List<GenreGetListViewModel>>(datas);

            }
            return View(list);
        }
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GenreCreateViewModel genreCreateViewModel)
        {
            var dataStr = JsonConvert.SerializeObject(genreCreateViewModel);
            var stringContent = new StringContent(dataStr,Encoding.UTF8,"application/json");
            var response = await _httpClient.PostAsync(baseAddress + "/genres/create", stringContent);


            if(response.IsSuccessStatusCode)
            {
                TempData["Successed"] = "Genre successfully create!";
                return RedirectToAction("Index");
            }
            return View();

        }

    }
}
