﻿using Microsoft.EntityFrameworkCore;
using Wedding.Data;
using Wedding.Data.Entities;

namespace Wedding.Services
{
    public class PartyService : IPartyService
    {

        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        public PartyService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {

            _contextFactory = contextFactory;
        }

        public async Task<List<Party>> GetPartiesByPartyName(string partyName)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            var parties = await Task.FromResult(context.Party.Where(p => p.PartyName.Contains(partyName, StringComparison.OrdinalIgnoreCase)).ToList());
            return parties;
        }
        public async Task<List<Party>> GetAllAsync()
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            var parties =  await context.Party.ToListAsync();
            return parties;
        }
        public async Task<List<Party>> GetAllAsync(bool includeGuests = true)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            var parties = await context.Party.Include(x =>x.Guests).ToListAsync();
            return parties;
        }

        public async Task<Party> GetByIdAsync(Guid id)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Party.FindAsync(id);
        }

        public async Task<Party> GetByUniqueInviteIdAsync(string id)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Party.Include(p => p.Guests)
            .FirstOrDefaultAsync(p => p.UniqueInviteId == id);
        }

        public async Task AddAsync(Party party)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            context.Party.Add(party);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Party party)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            if (party.Guests != null)
            {
                foreach (var guests in party.Guests)
                {
                    context.Guests.Update(guests);
                }
            }

            context.Party.Update(party);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Party party)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            context.Party.Remove(party);
            await context.SaveChangesAsync();
        }

        public async Task<string> GenerateUniqueInviteIdAsync()
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_~";
            var length = 8;
            var inviteId = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return inviteId;
        }
    }
}
