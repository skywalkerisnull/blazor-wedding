using Microsoft.EntityFrameworkCore;
using MudBlazor;
using System.Linq;
using Wedding.Data;
using Wedding.Data.Entities;
using Wedding.Models;

namespace Wedding.Services
{
    public class GuestService : IGuestService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public GuestService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Guest>> GetAllAsync()
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Guests.ToListAsync();
        }

        public async Task<Guest> GetByIdAsync(Guid id)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Guests.FindAsync(id);
        }

        public async Task<List<Guest>> GetByPartyIdAsync(Guid partyId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Guests.Where(g => g.PartyId == partyId).ToListAsync();
        }

        public async Task AddAsync(Guest guest)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            context.Guests.Add(guest);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guest guest)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            context.Guests.Update(guest);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guest guest)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            context.Guests.Remove(guest);
            await context.SaveChangesAsync();
        }

        //    public GuestService(ApplicationDbContext context)
        //    {
        //        context = context;
        //    }

        //    public async Task<PagedResult<Guest>> GetPagedResultAsync(int skip, int take, string orderBy, SortDirection orderDirection)
        //    {
        //        var query = context.Guests.AsQueryable();

        //        if (!string.IsNullOrEmpty(orderBy))
        //        {
        //            query = orderDirection == SortDirection.Ascending ? query.OrderBy(g => g.GetType().GetProperty(orderBy).GetValue(g)) : query.OrderByDescending(g => g.GetType().GetProperty(orderBy).GetValue(g));
        //        }

        //        var totalItems = await query.CountAsync();
        //        var items = await query.Skip(skip).Take(take).ToListAsync();

        //        return new PagedResult<Guest>
        //        {
        //            TotalCount = totalItems,
        //            Items = items
        //        };
        //    }

        //    public async Task<Guest> GetByIdAsync(Guid id)
        //    {
        //        return await context.Guests.FindAsync(id);
        //    }

        //    public async Task AddAsync(Guest guest)
        //    {
        //        guest.GuestId = Guid.NewGuid();
        //        await context.Guests.AddAsync(guest);
        //        await context.SaveChangesAsync();
        //    }

        //    public async Task UpdateAsync(Guest guest)
        //    {
        //        context.Guests.Update(guest);
        //        await context.SaveChangesAsync();
        //    }

        //    public async Task DeleteAsync(Guid id)
        //    {
        //        var guest = await context.Guests.FindAsync(id);
        //        if (guest != null)
        //        {
        //            context.Guests.Remove(guest);
        //            await context.SaveChangesAsync();
        //        }
        //    }
        //}
    }
}
