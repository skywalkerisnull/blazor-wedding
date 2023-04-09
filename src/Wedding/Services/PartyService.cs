using Wedding.Data.Entities;

namespace Wedding.Services
{
    public class PartyService : IPartyService
    {
        private static readonly List<Party> Parties = new List<Party>
    {
        new Party { PartyId = Guid.NewGuid(), PartyName = "Smith Family", Address = "123 Main Street", Comments = "No comments", IsInvited = true, InvitationOpened = true, InvitationSent = true, UniqueInviteId = "ABC123", InviteSentDate = DateTime.Now.AddDays(-14) },
        new Party { PartyId = Guid.NewGuid(), PartyName = "Johnson Family", Address = "456 Elm Street", Comments = "No comments", IsInvited = true, InvitationOpened = false, InvitationSent = true, UniqueInviteId = "DEF456", InviteSentDate = DateTime.Now.AddDays(-10) },
        new Party { PartyId = Guid.NewGuid(), PartyName = "Williams Family", Address = "789 Oak Street", Comments = "No comments", IsInvited = false, InvitationOpened = false, InvitationSent = false, UniqueInviteId = "GHI789", InviteSentDate = DateTime.MinValue }
    };

        public Task<List<Party>> GetPartiesAsync()
        {
            return Task.FromResult(Parties);
        }

        public Task AddPartyAsync(Party party)
        {
            Parties.Add(party);
            return Task.CompletedTask;
        }
    }
}
