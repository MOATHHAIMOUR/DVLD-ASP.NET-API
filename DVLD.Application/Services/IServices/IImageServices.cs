using Microsoft.AspNetCore.Http;

namespace DVLD.Application.Services.IServices
{
    public interface IImageServices
    {
        public Task<string?> UploadImageAsync(IFormFile file);

        public Task<bool> DeleteImageAsync(string imageUrl);
        public Task<bool> IsImageExists(string imageUrl);


    }
}
