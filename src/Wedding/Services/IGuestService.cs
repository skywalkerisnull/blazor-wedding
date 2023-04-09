using Wedding.Data.Entities;

namespace Wedding.Services
{
    public interface IGuestService
    {
        Task<List<Guest>> GetGuestsAsync();
        Task AddGuestAsync(Guest guest);
    }
}
