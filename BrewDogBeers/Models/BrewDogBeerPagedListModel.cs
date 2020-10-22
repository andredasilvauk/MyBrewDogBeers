using BrewDogBeersData;
using System.Collections.Generic;

namespace BrewDogBeers.Models
{
    public class BrewDogBeerPagedListModel
    {
        public IEnumerable<BrewDogBeerModel> BrewDogBeerModels { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
