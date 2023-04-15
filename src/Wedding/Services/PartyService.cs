using Microsoft.EntityFrameworkCore;
using Wedding.Data;
using Wedding.Data.Entities;

namespace Wedding.Services
{
    public class PartyService : IPartyService
    {
        private readonly ApplicationDbContext _context;
        public PartyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Party>> GetAllAsync()
        {
            var parties =  await _context.Party.ToListAsync();
            return parties;
        }

        public async Task<Party> GetByIdAsync(Guid id)
        {
            return await _context.Party.FindAsync(id);
        }

        public async Task AddAsync(Party party)
        {
            _context.Party.Add(party);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Party party)
        {
            _context.Party.Update(party);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Party party)
        {
            _context.Party.Remove(party);
            await _context.SaveChangesAsync();
        }

        public async Task<string> GenerateUniqueInviteIdAsync()
        {
            // This is a mock method to generate a random string
            // You can replace it with your own logic
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var length = 8;
            var inviteId = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return inviteId;
        }
    }
}
