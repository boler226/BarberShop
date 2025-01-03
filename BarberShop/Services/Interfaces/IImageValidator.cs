namespace BarberShop.Services.Interfaces
{
    public interface IImageValidator
    {
        Task<bool> IsValidImageAsync(IFormFile image, CancellationToken cancellationToken);
        Task<bool> IsValidNullPossibleImageAsync(IFormFile? image, CancellationToken cancellationToken);

        Task<bool> IsValidImagesAsync(IEnumerable<IFormFile> images, CancellationToken cancellationToken);
        Task<bool> IsValidNullPossibeImagesAsync(IEnumerable<IFormFile>? images, CancellationToken cancellationToken);
    }
}
