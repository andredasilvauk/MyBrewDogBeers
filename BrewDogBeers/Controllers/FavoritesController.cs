using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BrewDogBeers.Models;
using BrewDogBeersData;
using System.Diagnostics;

namespace BrewDogBeers.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBrewDogBeersDataManager _dataManager;
        private const int _rowNumber = 10;

        public FavoritesController(ILogger<HomeController> logger, IBrewDogBeersDataManager dataManager)
        {
            _logger = logger;
            _dataManager = dataManager;
        }

        public IActionResult Index()
        {
            return View(new BrewDogBeerFavoriteListModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email)
        {
            var viewData = new BrewDogBeerFavoriteListModel()
            {            
                Email = email
            };

            if (!string.IsNullOrEmpty(email))
            {
                var data = await _dataManager.GetFavoriteBeers(email);

                var dataModel = data.Select(x => new BrewDogBeerModel(x));

                viewData.BrewDogBeerModels = dataModel;
            }
            return View(viewData);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
