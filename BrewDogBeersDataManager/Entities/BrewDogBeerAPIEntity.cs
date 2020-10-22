using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BrewDogBeersData
{
    public class BrewDogBeerAPIEntity
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("tagline")]
        public string TagLine { get; set; }

        [JsonPropertyName("first_brewed")]
        public string FirstBrewed { get; set; }
        
        [JsonPropertyName("description")]
        public string Description { get; set; }
        
        [JsonPropertyName("image_url")]
        public string ImageURL { get; set; }
        
        [JsonPropertyName("abv")]
        public decimal? ABV{ get; set; }
        
        [JsonPropertyName("ibu")]
        public decimal? IBU { get; set; }
        
        [JsonPropertyName("target_fg")]
        public decimal? TargetFG { get; set; }
        
        [JsonPropertyName("target_og")]
        public decimal? TargetOG { get; set; }
        
        [JsonPropertyName("ebc")]
        public decimal? EBC { get; set; }
        
        [JsonPropertyName("srm")]
        public decimal? SEM { get; set; }
        
        [JsonPropertyName("ph")]
        public decimal? PH { get; set; }
        
        [JsonPropertyName("attenuation_level")]
        public decimal? AttenuationLevel { get; set; }
        
        [JsonPropertyName("volume")]
        public ValueUnitType Volume { get; set; }
        
        [JsonPropertyName("boil_volume")]
        public ValueUnitType BoilVolume { get; set; }
        
        [JsonPropertyName("method")]
        public MethodType Method { get; set; }
        
        [JsonPropertyName("ingredients")]
        public IngredientsType Ingredients { get; set; }
        
        [JsonPropertyName("food_pairing")]
        public IEnumerable<string> FoodPairing { get; set; }
        
        [JsonPropertyName("brewers_tips")]
        public string BrewersTips { get; set; }
        
        [JsonPropertyName("contributed_by")]
        public string ContributedBy { get; set; }
    }

    public class ValueUnitType
    {
        [JsonPropertyName("value")]
        public decimal? Value { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }

    public class MashTempType
    {
        [JsonPropertyName("temp")]
        public ValueUnitType Temp { get; set; }

        [JsonPropertyName("duration")]
        public decimal? Duration { get; set; }
    }

    public class FermentationType
    {
        [JsonPropertyName("temp")]
        public ValueUnitType Temp { get; set; }
    }

    public class MethodType
    {
        [JsonPropertyName("mash_temp")]
        public IEnumerable<MashTempType> MashTemp { get; set; }

        [JsonPropertyName("fermentation")]
        public FermentationType Fermentation { get; set; }

        [JsonPropertyName("twist")]
        public string Twist { get; set; }
    }

    public class MaltType
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("amount")]
        public ValueUnitType Amount { get; set; }
    }

    public class HopsType
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("amount")]
        public ValueUnitType Amount { get; set; }

        [JsonPropertyName("add")]
        public string Add { get; set; }

        [JsonPropertyName("attribute")]
        public string Attribute { get; set; }
    }

    public class IngredientsType
    {
        [JsonPropertyName("malt")]
        public IEnumerable<MaltType> Malt { get; set; }

        [JsonPropertyName("hops")]
        public IEnumerable<HopsType> Hops { get; set; }

        [JsonPropertyName("yeast")]
        public string Yeast { get; set; }
    }
}