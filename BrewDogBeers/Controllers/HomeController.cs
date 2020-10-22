using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BrewDogBeers.Models;
using BrewDogBeersData;
using System.Collections.Generic;

namespace BrewDogBeers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBrewDogBeersDataManager _dataManager;
        private const int _rowNumber = 10;

        public HomeController(ILogger<HomeController> logger, IBrewDogBeersDataManager dataManager)
        {
            _logger = logger;
            _dataManager = dataManager;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _dataManager.GetBeers(1, _rowNumber);

            var numberOfBeers = await _dataManager.GetTotalNumberOfBeers();

            var dataModel = data.Select(x => new BrewDogBeerModel(x));

            var viewData = new BrewDogBeerPagedListModel()
            {
                BrewDogBeerModels = dataModel,
                Pages = numberOfBeers / _rowNumber,
                CurrentPage = 1
            };
           
            return View(viewData);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int pageNumber, string beerName)
        {            
            var data = string.IsNullOrEmpty(beerName) ? await _dataManager.GetBeers(pageNumber, _rowNumber) : await _dataManager.GetBeersByName(beerName, pageNumber, _rowNumber);

            var numberOfBeers = await _dataManager.GetTotalNumberOfBeers();

            var dataModel = data.Select(x => new BrewDogBeerModel(x));

            var viewData = new BrewDogBeerPagedListModel()
            {
                BrewDogBeerModels = dataModel,
                Pages = numberOfBeers / _rowNumber,
                CurrentPage = pageNumber
            };

            TempData["pageNumber"] = pageNumber;
            TempData["beerName"] = beerName;

            return View(viewData);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite(string email, string currentSelection)
        {
            var data = await _dataManager.AddFavoriteBeers(email, currentSelection);

            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var data = await _dataManager.GetBeerById(id);

            return View(new BrewDogBeerModel(data));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
