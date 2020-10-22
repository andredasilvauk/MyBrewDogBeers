using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrewDogBeersData
{
    public interface IBrewDogBeersFavorites
    {
        Task<IEnumerable<int>> GetFavoriteBrewDogBeers(string email);
        Task<int> GetFavoriteBrewDogBeersCount(string email);
        Task<AddFavoriteBeersResult> AddFavoriteBrewDogBeers(string email, string beerIds);
        Task DeleteFavoriteBrewDogBeer(string email, int beerId);
    }
}
