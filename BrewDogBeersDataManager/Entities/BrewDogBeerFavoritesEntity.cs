using Newtonsoft.Json;
using System.Collections.Generic;

namespace BrewDogBeersData
{
    public class BrewDogBeerFavoritesEntity
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("beerIds")]
        public IEnumerable<BrewDogFavoriteIdEntity> BeerIds { get; set; }
    }

    public class BrewDogFavoriteIdEntity
    {
        [JsonProperty("beerId")]
        public int BeerId { get; set; }
    }
}
