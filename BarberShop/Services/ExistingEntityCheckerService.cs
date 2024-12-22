using BarberShop.Database.Context;
using BarberShop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Services
{
    public class ExistingEntityCheckerService(
        DataContext context
        ) : IExistingEntityCheckerService {
        
        public async Task<bool> IsCorrectCountryId(long id, CancellationToken cancellationToken) =>
                await context.Countries.AnyAsync(c => c.Id == id, cancellationToken);

        public async Task<bool> IsCorrectCityId(long id, CancellationToken cancellationToken) =>
                await context.Cities.AnyAsync(c => c.Id == id, cancellationToken);

        public async Task<bool> IsCorrectAddressId(long id, CancellationToken cancellationToken) =>
                await context.Addresses.AnyAsync(a => a.Id == id, cancellationToken);

        public async Task<bool> IsCorrectAffiliateId(long id, CancellationToken cancellationToken) =>
                await context.Affiliates.AnyAsync(a => a.Id == id, cancellationToken);

        public async Task<bool> IsCorrectBarbershopId(long id, CancellationToken cancellationToken) =>
                await context.Barbershops.AnyAsync(b => b.Id == id, cancellationToken);

        public async Task<bool> IsCorrectCommentId(long id, CancellationToken cancellationToken) =>
                await context.Comments.AnyAsync(c => c.Id == id, cancellationToken);

        public async Task<bool> IsCorrectEmployeeId(long id, CancellationToken cancellationToken) =>
                await context.Employees.AnyAsync(e => e.Id == id, cancellationToken);

        public async Task<bool> IsCorrectPositionId(long id, CancellationToken cancellationToken) =>
                await context.Positions.AnyAsync(p => p.Id == id, cancellationToken);

        public async Task<bool> IsCorrectReservationId(long id, CancellationToken cancellationToken) =>
                await context.Reservations.AnyAsync(r => r.Id == id, cancellationToken);

        public async Task<bool> IsCorrectServiceId(long id, CancellationToken cancellationToken) =>
                await context.Services.AnyAsync(s => s.Id == id, cancellationToken);

        public async Task<bool> IsCorrectUserId(long id, CancellationToken cancellationToken) => 
                await context.Users.AnyAsync(u => u.Id == id, cancellationToken);
    }
}
