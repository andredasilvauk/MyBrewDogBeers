using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrewDogBeersData
{
    public class BrewDogBeersFavorites : IBrewDogBeersFavorites
    {
        private Container _container;
        private readonly AppSettings _settings;

        public BrewDogBeersFavorites(CosmosClient client, string database, string container, AppSettings settings)
        {
            _container = client.GetContainer(database, container);
            _settings = settings;
        }

        public async Task<IEnumerable<int>> GetFavoriteBrewDogBeers(string email)
        {
            string query = $"SELECT zc.beerId FROM c JOIN zc IN c.beerIds WHERE c.id = '{email}'";
            List<int> beerIds = new List<int>();

            FeedIterator<BrewDogFavoriteIdEntity> feedIterator = _container.GetItemQueryIterator<BrewDogFavoriteIdEntity>(new QueryDefinition(query));

            FeedResponse<BrewDogFavoriteIdEntity> resultSet;
            while (feedIterator.HasMoreResults)            
            {
                resultSet = await feedIterator.ReadNextAsync();
                beerIds = resultSet.Select(x => x.BeerId).ToList();
            }

            return beerIds;
        }

        public async Task<int> GetFavoriteBrewDogBeersCount(string email)
        {
            string query = $"SELECT COUNT(1) NUM FROM c JOIN zc IN c.beerIds WHERE c.id = '{email}'";                      

            FeedIterator<Test> feedIterator = _container.GetItemQueryIterator<Test>(new QueryDefinition(query));

            if (feedIterator.HasMoreResults == false)
            {
                return 0;
            }
            
            FeedResponse<Test> resultSet = await feedIterator.ReadNextAsync();            
            return 1;
                    
        }

        public async Task<AddFavoriteBeersResult> AddFavoriteBrewDogBeers(string email, string beerIds)
        {
            if (await GetFavoriteBrewDogBeersCount(email) == _settings.MaxNumberOfFavorites)
            {
                return AddFavoriteBeersResult.TooManyBeerFavorites;
            }

            beerIds = beerIds.Trim('|');

            var newEntries = beerIds.Split('|').Select(x => Convert.ToInt32(x));

            var currentList = await GetFavoriteBrewDogBeers(email);

            currentList = currentList.Union(newEntries);

            var favoriteBeers = new BrewDogBeerFavoritesEntity()
            {
                Id = email,
                BeerIds = currentList.Select(x => new BrewDogFavoriteIdEntity() { BeerId = x })
            };

            var result = await _container.UpsertItemAsync<BrewDogBeerFavoritesEntity>(favoriteBeers, new PartitionKey(favoriteBeers.Id));

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return AddFavoriteBeersResult.Success;
            }
            else
            {
                return AddFavoriteBeersResult.FailureUpdatingRecord;
            }
        }

        public async Task DeleteFavoriteBrewDogBeer(string email, int beerId)
        {
            var currentList = await GetFavoriteBrewDogBeers(email);

            var newList = currentList.ToList();

            newList.Remove(beerId);

            var favoriteBeers = new BrewDogBeerFavoritesEntity()
            {
                Id = email,
                BeerIds = newList.Select(x => new BrewDogFavoriteIdEntity() { BeerId = x })
            };

            await _container.UpsertItemAsync(favoriteBeers);
        }

        private class Test
        {
            public int NUM { get; set; }
        }
    }
}
