using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RickAndMortyCharacters.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RickAndMortyCharacters.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://rickandmortyapi.com/api/character");
            RMApiResponse responseDeserialized = JsonConvert.DeserializeObject<RMApiResponse>(
                await response.Content.ReadAsStringAsync());
            //Console.WriteLine(await response.Content.ReadAsStringAsync());
            // Console.WriteLine(responseDeserialized.results);
            ViewBag.Characters = responseDeserialized.results;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
