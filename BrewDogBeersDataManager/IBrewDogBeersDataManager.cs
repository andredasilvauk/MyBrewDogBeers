using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrewDogBeersData
{
    public interface IBrewDogBeersDataManager
    {
        Task<int> GetTotalNumberOfBeers();
        Task<IEnumerable<BrewDogBeerAPIEntity>> GetBeers(int page, int perPage);
        Task<IEnumerable<BrewDogBeerAPIEntity>> GetBeersByName(string beerName, int pageNumber, int perPage);
        Task<BrewDogBeerAPIEntity> GetBeerById(int beerId);
        Task<IEnumerable<BrewDogBeerAPIEntity>> GetFavoriteBeers(string email);
        Task<AddFavoriteBeersResult> AddFavoriteBeers(string email, string beerIds);
    }
}
