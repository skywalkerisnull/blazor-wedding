using Microsoft.EntityFrameworkCore;
using Wedding.Data;
using Wedding.Data.Entities;

namespace Wedding.Services
{
    public class AccomodationService : IAccomodationService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public AccomodationService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task AddAsync(AccomodationOptions accomodationOptions)
        {
            // Validate the input
            if (accomodationOptions == null)
            {
                throw new ArgumentNullException(nameof(accomodationOptions));
            }

            // Create a new context using the factory
            await using var context = await _contextFactory.CreateDbContextAsync();

            // Add the accomodation options to the context
            await context.AccomodationOptions.AddAsync(accomodationOptions);

            // Save the changes to the database
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AccomodationOptions accomodationOptions)
        {
            // Validate the input
            if (accomodationOptions == null)
            {
                throw new ArgumentNullException(nameof(accomodationOptions));
            }

            // Create a new context using the factory
            await using var context = await _contextFactory.CreateDbContextAsync();

            // Find the accomodation options in the context by id
            var existing = await context.AccomodationOptions.FindAsync(accomodationOptions.AccomodationOptionsId);

            // If the accomodation options exist, remove them from the context
            if (existing != null)
            {
                context.AccomodationOptions.Remove(existing);
            }

            // Save the changes to the database
            await context.SaveChangesAsync();
        }

        public async Task<List<AccomodationOptions>> GetAllAsync()
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.AccomodationOptions.ToListAsync();
        }

        public async Task<AccomodationOptions> GetByIdAsync(Guid accomodationId)
        {
            // Validate the input
            if (accomodationId == Guid.Empty)
            {
                throw new ArgumentException("Invalid accomodation id", nameof(accomodationId));
            }

            // Create a new context using the factory
            await using var context = await _contextFactory.CreateDbContextAsync();

            // Find and return the accomodation options in the context by id
            return await context.AccomodationOptions.FindAsync(accomodationId);
        }

        public async Task UpdateAsync(AccomodationOptions accomodationOptions)
        {
            // Validate the input
            if (accomodationOptions == null)
            {
                throw new ArgumentNullException(nameof(accomodationOptions));
            }

            // Create a new context using the factory
            await using var context = await _contextFactory.CreateDbContextAsync();

            // Find the accomodation options in the context by id
            var existing = await GetByIdAsync(accomodationOptions.AccomodationOptionsId);

            // If the accomodation options exist, update their properties
            if (existing != null)
            {
                existing.WeddingSetup = accomodationOptions.WeddingSetup;
                existing.AccomodationName = accomodationOptions.AccomodationName;
                existing.AccomodationType = accomodationOptions.AccomodationType;
                existing.AccomodationDescription = accomodationOptions.AccomodationDescription;
                existing.AccomodationUrl = accomodationOptions.AccomodationUrl;
                existing.PhoneNumber = accomodationOptions.PhoneNumber;
                existing.Picture = accomodationOptions.Picture;
            }
            // Save the changes to the database
            await context.SaveChangesAsync();
        }
        public async Task UpdateAsync(IList<AccomodationOptions> accomodationOptions)
        {
            foreach (var accomodation in accomodationOptions)
            {
                await UpdateAsync(accomodation);
            }
        }
    }
}
