using MudBlazor;
using System.Linq.Dynamic.Core;
using Wedding.Data.Entities;

namespace Wedding.Services
{
    public interface IAccomodationService
    {
        //Task<PagedResult<AccomodationOptions>> GetPagedResultAsync(int skip, int take, string orderBy, SortDirection orderDirection);
        Task<List<AccomodationOptions>> GetAllAsync();
        Task<AccomodationOptions> GetByIdAsync(Guid accomodationId);
        Task AddAsync(AccomodationOptions accomodationOptions);
        Task UpdateAsync(AccomodationOptions accomodationOptions);
        Task UpdateAsync(IList<AccomodationOptions> accomodationOptions);
        Task DeleteAsync(AccomodationOptions accomodationOptions);
    }
}
