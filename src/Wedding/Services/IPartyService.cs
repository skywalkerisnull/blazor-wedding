using Wedding.Data.Entities;

namespace Wedding.Services
{
    public interface IPartyService
    {
        Task<List<Party>> GetPartiesAsync();
        Task AddPartyAsync(Party party);
    }
}
