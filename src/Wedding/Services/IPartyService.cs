using Wedding.Data.Entities;

namespace Wedding.Services
{
    public interface IPartyService
    {
        //Task<List<Party>> GetPartiesAsync();
        //Task AddPartyAsync(Party party);

        Task<List<Party>> GetAllAsync();
        Task<List<Party>> GetAllAsync(bool includeGuests);
        Task<Party> GetByIdAsync(Guid id);
        Task<Party> GetByUniqueInviteIdAsync(string id);
        Task AddAsync(Party party);
        Task UpdateAsync(Party party);
        Task DeleteAsync(Party party);
        Task<string> GenerateUniqueInviteIdAsync();
    }
}
