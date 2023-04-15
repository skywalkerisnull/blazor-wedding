using MudBlazor;
using Wedding.Data.Entities;
using Wedding.Models;

namespace Wedding.Services
{
    interface IGuestService
    {
        Task<PagedResult<Guest>> GetPagedResultAsync(int skip, int take, string orderBy, SortDirection orderDirection);
        Task<Guest> GetByIdAsync(Guid id);
        Task AddAsync(Guest guest);
        Task UpdateAsync(Guest guest);
        Task DeleteAsync(Guid id);
    }
}
