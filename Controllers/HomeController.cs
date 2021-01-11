using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RickAndMortyCharacters.Models;
using System.Diagnostics;
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

        private async Task<bool> fetchAllCharacters(HttpClient client)
        {
            RMApiResponse characterPage = await fetchCharacterPage(client, "https://rickandmortyapi.com/api/character");
            var Characters = characterPage.results;
            do
            {
                characterPage = await fetchCharacterPage(client, characterPage.info.next);
                Characters.AddRange(characterPage.results);
            }
            while (characterPage.info.next != null);
            ViewBag.Characters = Characters;
            return true;
        }

        private async Task<RMApiResponse> fetchCharacterPage(HttpClient client, string url)
        {
            HttpResponseMessage response = await client.GetAsync(url);
            RMApiResponse responseDeserialized = JsonConvert.DeserializeObject<RMApiResponse>(
                await response.Content.ReadAsStringAsync()
                );
            return responseDeserialized;
        }
        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();
            if (await fetchAllCharacters(client))
            {
                return View();
            }
            else
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

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
