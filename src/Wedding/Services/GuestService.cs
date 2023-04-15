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
        private readonly ApplicationDbContext _context;

        public GuestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Guest>> GetAllAsync()
        {
            return await _context.Guests.ToListAsync();
        }

        public async Task<Guest> GetByIdAsync(Guid id)
        {
            return await _context.Guests.FindAsync(id);
        }

        public async Task<List<Guest>> GetByPartyIdAsync(Guid partyId)
        {
            return await _context.Guests.Where(g => g.PartyId == partyId).ToListAsync();
        }

        public async Task AddAsync(Guest guest)
        {
            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guest guest)
        {
            _context.Guests.Update(guest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guest guest)
        {
            _context.Guests.Remove(guest);
            await _context.SaveChangesAsync();
        }

        //    public GuestService(ApplicationDbContext context)
        //    {
        //        _context = context;
        //    }

        //    public async Task<PagedResult<Guest>> GetPagedResultAsync(int skip, int take, string orderBy, SortDirection orderDirection)
        //    {
        //        var query = _context.Guests.AsQueryable();

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
        //        return await _context.Guests.FindAsync(id);
        //    }

        //    public async Task AddAsync(Guest guest)
        //    {
        //        guest.GuestId = Guid.NewGuid();
        //        await _context.Guests.AddAsync(guest);
        //        await _context.SaveChangesAsync();
        //    }

        //    public async Task UpdateAsync(Guest guest)
        //    {
        //        _context.Guests.Update(guest);
        //        await _context.SaveChangesAsync();
        //    }

        //    public async Task DeleteAsync(Guid id)
        //    {
        //        var guest = await _context.Guests.FindAsync(id);
        //        if (guest != null)
        //        {
        //            _context.Guests.Remove(guest);
        //            await _context.SaveChangesAsync();
        //        }
        //    }
        //}
    }
}
