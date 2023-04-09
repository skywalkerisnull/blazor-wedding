using System.ComponentModel.DataAnnotations;

namespace Wedding.Data.Entities
{
    public class DietaryRequirements
    {
        [Required]
        [Key]
        public Guid DietaryRequirementsId { get; set; }
        public List<CommonDietaryRequirements> CommonRequirements { get; set;}
        public string? Allergies { get; set; }
        public string? Other { get; set; }
    }

    public enum CommonDietaryRequirements
    {
        None = 0,
        Vegetarian = 1,
        Vegan = 2,
        LactoseIntollerant = 3,
        GluentFree = 4,
        Kosher = 5,
        Diabetic = 6,
        DairyFree = 7,
        Pescetarians = 8,
        PeanutAllergy = 9,
        ShellfishAllergy =10,
        Hala = 11,

        Other = 99,
    }
}