using System.ComponentModel.DataAnnotations;

namespace Wedding.Data.Entities
{
    public class Party
    {
        [Required]
        [Key]
        public Guid PartyId { get; set; }
        public string PartyName { get; set; }
        public List<Guest> Guests { get; set; }
        public string? Address { get; set; }

        public string? Comments { get; set; }
        public bool IsInvited { get; set; } = true;
        public bool InvitationOpened { get; set; } = false;
        public bool InvitationSent { get; set; } = false;

        [Required]
        public string UniqueInviteId { get; set; }

        public DateTime InviteSentDate { get; set; }
    }
}
