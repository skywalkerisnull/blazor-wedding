using System.ComponentModel.DataAnnotations;

namespace Wedding.Data.Entities
{
    public class Guest
    {
        [Required]
        [Key]
        public Guid GuestId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public bool? IsAttending { get; set; }
        public bool? IsAttendingRehersalDinner { get; set; }
        public DateTime? InviteAccepted { get; set; }
        public DateTime? InvitationOpened { get; set; }
        public AgeBracket AgeBracket { get; set; }
        public List<CommonDietaryRequirements> CommonRequirements { get; set; }
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
        ShellfishAllergy = 10,
        Hala = 11,
        Other = 99,
    }

    public enum AgeBracket
    {
        Newborn = 0,
        OneAndThree = 1,
        ThreeAndSeven = 2,
        SevenAndEighteen = 3,
        EighteenPlus = 4,
    }
}
