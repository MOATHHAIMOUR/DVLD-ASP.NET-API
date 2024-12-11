using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DVLD.Application.Services.IServices;
using DVLD.Application.settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace DVLD.Application.Services
{
    public class ImageServices : IImageServices
    {
        private readonly Cloudinary _cloudinary;

        public ImageServices(IOptions<CloudinarySettings> cloudinarySettings)
        {
            _cloudinary = new Cloudinary(new Account(cloudinarySettings.Value.CloudName, cloudinarySettings.Value.ApiKey, cloudinarySettings.Value.ApiSecret));
        }

        public async Task<bool> DeleteImageAsync(string imageUrl)
        {
            string? publicId = ExtractPublicIdFromUrl(imageUrl);

            if (string.IsNullOrEmpty(publicId))
                return false;

            var deletionParams = new DeletionParams(publicId);

            var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

            return deletionResult.Result == "ok";
        }

        public async Task<bool> IsImageExists(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return false;

            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.SendAsync(new HttpRequestMessage(System.Net.Http.HttpMethod.Head, imageUrl));

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string?> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Transformation = new Transformation().Crop("fill").Gravity("face").Width(500).Height(500)
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return uploadResult?.SecureUrl?.AbsoluteUri;
        }

        private string? ExtractPublicIdFromUrl(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return null;

            // Remove query parameters if any
            var urlWithoutQuery = imageUrl.Split('?')[0];

            // Extract the last part of the URL
            var segments = urlWithoutQuery.Split('/');
            var fileNameWithExtension = segments.LastOrDefault();

            // Remove file extension to get the public ID
            var publicId = Path.GetFileNameWithoutExtension(fileNameWithExtension);

            return publicId;
        }
    }
}
