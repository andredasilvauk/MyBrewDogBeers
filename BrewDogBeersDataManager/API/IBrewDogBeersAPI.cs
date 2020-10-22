using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrewDogBeersData
{
    public interface IBrewDogBeersAPI
    {
        Task<int> GetTotalNumberOfBeers();
        Task<IEnumerable<BrewDogBeerAPIEntity>> GetBeersByName(string beerName, int pageNumber, int perPage);
        Task<BrewDogBeerAPIEntity> GetBeerById(int beerId);
        Task<IEnumerable<BrewDogBeerAPIEntity>> GetBeers(int page, int perPage);
        Task<IEnumerable<BrewDogBeerAPIEntity>> GetBeerByIds(IEnumerable<int> beerIds);
    }
}
