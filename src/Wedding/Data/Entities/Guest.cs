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
        public DateTime InviteAccepted { get; set; }
        public DateTime InvitationOpened { get; set; }
        public AgeBracket AgeBracket { get; set; }
        public DietaryRequirements? DietaryRequirements { get; set; }
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
