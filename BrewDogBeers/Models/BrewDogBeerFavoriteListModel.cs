using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BrewDogBeers.Models
{
    public class BrewDogBeerFavoriteListModel
    {
        public IEnumerable<BrewDogBeerModel> BrewDogBeerModels { get; set; }

        [EmailAddress]    
        public string Email { get; set; }
    }
}
