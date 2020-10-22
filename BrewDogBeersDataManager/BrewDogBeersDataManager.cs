using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrewDogBeersData
{
    public class BrewDogBeersDataManager : IBrewDogBeersDataManager
    {
        private int _totalNumberOfBeers = 0;
        private IBrewDogBeersAPI _brewDogBeersAPI;
        private IBrewDogBeersFavorites _brewDogBeersFavorites;

        public BrewDogBeersDataManager(IBrewDogBeersAPI brewDogBeersAPI, IBrewDogBeersFavorites brewDogBeersFavorites)
        {
            _brewDogBeersAPI = brewDogBeersAPI;
            _brewDogBeersFavorites = brewDogBeersFavorites;
        }
        public async Task<int> GetTotalNumberOfBeers()
        {
            if (_totalNumberOfBeers == 0)
            {
                _totalNumberOfBeers = await _brewDogBeersAPI.GetTotalNumberOfBeers();
            }

            return _totalNumberOfBeers;
        }

        public async Task<IEnumerable<BrewDogBeerAPIEntity>> GetBeers(int page, int perPage)
        {
            return await _brewDogBeersAPI.GetBeers(page, perPage);
        }

        public async Task<IEnumerable<BrewDogBeerAPIEntity>> GetBeersByName(string beerName, int pageNumber, int perPage)
        {
            return await _brewDogBeersAPI.GetBeersByName(beerName, pageNumber, perPage);
        }

        public async Task<BrewDogBeerAPIEntity> GetBeerById(int beerId)
        {
            return await _brewDogBeersAPI.GetBeerById(beerId);
            
        }

        public async Task<IEnumerable<BrewDogBeerAPIEntity>> GetFavoriteBeers(string email)
        {
            var favoriteBeerIds = await _brewDogBeersFavorites.GetFavoriteBrewDogBeers(email);

            return await _brewDogBeersAPI.GetBeerByIds(favoriteBeerIds);
        }

        public async Task<AddFavoriteBeersResult> AddFavoriteBeers(string email, string beerIds)
        {
            return await _brewDogBeersFavorites.AddFavoriteBrewDogBeers(email, beerIds);
        }
    }
}
