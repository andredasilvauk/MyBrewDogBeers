using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace BrewDogBeersData
{
    public class BrewDogBeersAPI : IBrewDogBeersAPI
    {
        private readonly AppSettings _settings;
        private IHttpClientFactory _clientFactory;        

        public BrewDogBeersAPI(AppSettings settings, IHttpClientFactory clientFactory)
        {
            this._settings = settings;
            this._clientFactory = clientFactory;
        }

        public async Task<BrewDogBeerAPIEntity> GetBeerById(int beerId)
        {
            var response = await GetResponse($"/v2/beers/{beerId}");

            var responseStream = await response.Content.ReadAsStreamAsync();

            var beer = await JsonSerializer.DeserializeAsync<IEnumerable<BrewDogBeerAPIEntity>>(responseStream);

            return beer.FirstOrDefault();
        }

        public async Task<IEnumerable<BrewDogBeerAPIEntity>> GetBeerByIds(IEnumerable<int> beerIds)
        {
            var response = await GetResponse($"?ids={string.Join('|',beerIds)}");

            var responseStream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<IEnumerable<BrewDogBeerAPIEntity>>(responseStream);
        }

        public async Task<IEnumerable<BrewDogBeerAPIEntity>> GetBeers(int page, int perPage)
        {
            var response = await GetResponse($"?page={page}&per_page={perPage}");

            var responseStream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<IEnumerable<BrewDogBeerAPIEntity>>(responseStream);
        }

        public async Task<IEnumerable<BrewDogBeerAPIEntity>> GetBeersByName(string beerName, int pageNumber, int perPage)
        {
            var response = await GetResponse($"?beer_name={beerName.Replace(' ', '_')}&page={pageNumber}&per_page={perPage}");

            var responseStream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<IEnumerable<BrewDogBeerAPIEntity>>(responseStream);
        }

        public async Task<int> GetTotalNumberOfBeers()
        {
            int numberOfBeers = 0;
            int pageNumber = 1;

            var beers = await GetBeers(pageNumber, 50);

            while (beers.Count() > 0)
            {
                numberOfBeers += beers.Count();
                pageNumber++;
                beers = await GetBeers(pageNumber, 50);
            }

            return numberOfBeers;
        }

        private async Task<HttpResponseMessage> GetResponse(string apiParams)
        {
            Uri apiUrl = null;

            if (Uri.TryCreate(new Uri(_settings.APIUrl), apiParams, out apiUrl))
            {
                var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

                var client = _clientFactory.CreateClient();

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                else
                {
                    throw new InvalidOperationException("Invalid response from API!");
                }
            }
            else
            {
                throw new InvalidOperationException("Unable to create Uri for API call!");
            }
        }


    }
}
