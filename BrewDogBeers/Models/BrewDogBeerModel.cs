using BrewDogBeersData;

namespace BrewDogBeers.Models
{
    public class BrewDogBeerModel
    {
        public BrewDogBeerModel()
        {

        }
        public BrewDogBeerModel(BrewDogBeerAPIEntity entity)
        {

            this.Id = entity.Id;
            this.Name = entity.Name;
            this.TagLine = entity.TagLine;
            this.FirstBrewed = entity.FirstBrewed;
            this.Description = entity.Description;
            this.ImageURL = entity.ImageURL;
            this.ABV = entity.ABV;
            this.IBU = entity.IBU;
            this.TargetFG = entity.TargetFG;
            this.TargetOG = entity.TargetOG;
            this.EBC = entity.EBC;
            this.SEM = entity.SEM;
            this.PH = entity.PH;
            this.AttenuationLevel = entity.AttenuationLevel;
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string TagLine { get; set; }

        public string FirstBrewed { get; set; }

        public string Description { get; set; }

        public string ImageURL { get; set; }

        public decimal? ABV { get; set; }

        public decimal? IBU { get; set; }

        public decimal? TargetFG { get; set; }

        public decimal? TargetOG { get; set; }

        public decimal? EBC { get; set; }

        public decimal? SEM { get; set; }

        public decimal? PH { get; set; }

        public decimal? AttenuationLevel { get; set; }

        //public ValueUnitType Volume { get; set; }

        //public ValueUnitType BoilVolume { get; set; }

        //public MethodType Method { get; set; }

        //public IngredientsType Ingredients { get; set; }

        //public IEnumerable<string> FoodPairing { get; set; }

        //public string BrewersTips { get; set; }

        //public string ContributedBy { get; set; }

        public bool Checked { get; set; }
    }
}
