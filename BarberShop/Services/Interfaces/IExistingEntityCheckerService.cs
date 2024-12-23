namespace BarberShop.Services.Interfaces
{
    public interface IExistingEntityCheckerService {
        Task<bool> IsCorrectCountryId(long id, CancellationToken cancellationToken);
        Task<bool> IsCorrectCityId(long id, CancellationToken cancellationToken);
        Task<bool> IsCorrectAddressId(long id, CancellationToken cancellationToken);
        Task<bool> IsCorrectAffiliateId(long id, CancellationToken cancellationToken);
        Task<bool> IsCorrectBarbershopId(long id, CancellationToken cancellationToken);
        Task<bool> IsCorrectCommentId(long id, CancellationToken cancellationToken);
        Task<bool> IsCorrectEmployeeId(long id, CancellationToken cancellationToken);
        Task<bool> IsCorrectPositionId(int id, CancellationToken cancellationToken);
        Task<bool> IsCorrectReservationId(long id, CancellationToken cancellationToken);
        Task<bool> IsCorrectServiceId(long id, CancellationToken cancellationToken); 
        Task<bool> IsCorrectUserId(long id, CancellationToken cancellationToken);
    }
}
