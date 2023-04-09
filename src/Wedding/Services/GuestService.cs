using Wedding.Data.Entities;

namespace Wedding.Services
{
    public class GuestService : IGuestService
    {
        private static readonly List<Guest> Guests = new List<Guest>
    {
        new Guest { GuestId = Guid.NewGuid(), FirstName = "John", LastName = "Doe", IsAttending = true, IsAttendingRehersalDinner = true, InviteAccepted = DateTime.Now.AddDays(-7), InvitationOpened = DateTime.Now.AddDays(-14), AgeBracket = AgeBracket.EighteenPlus, DietaryRequirements = null },
        new Guest { GuestId = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe", IsAttending = true, IsAttendingRehersalDinner = false, InviteAccepted = DateTime.Now.AddDays(-3), InvitationOpened = DateTime.Now.AddDays(-10), AgeBracket = AgeBracket.EighteenPlus, DietaryRequirements = null },
        new Guest { GuestId = Guid.NewGuid(), FirstName = "Bob", LastName = "Smith", IsAttending = false, IsAttendingRehersalDinner = false, InviteAccepted = DateTime.MinValue, InvitationOpened = DateTime.Now.AddDays(-5), AgeBracket = AgeBracket.EighteenPlus, DietaryRequirements = null }
    };

        public Task<List<Guest>> GetGuestsAsync()
        {
            return Task.FromResult(Guests);
        }

        public Task AddGuestAsync(Guest guest)
        {
            Guests.Add(guest);
            return Task.CompletedTask;
        }
    }
}
