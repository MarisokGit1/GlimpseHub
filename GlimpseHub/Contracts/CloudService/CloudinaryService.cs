using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace GlimpseHub.CloudService
{
    public class CloudineryService : ICloudineryService
    {
        public Account account;
        private Cloudinary cloudinary;

        public CloudineryService()
        {
            account = new Account(
                      "dfaeymtxr",
                      "577989657892158",
                      "MQNkWbf4vbS8LGowJ0jlnJJXpfA");
            cloudinary = new Cloudinary(account);
        }

        public string Upload(IFormFile file)
        {
            // Upload the file to Cloudinary
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream())
            };
            var uploadResult = cloudinary.Upload(uploadParams);
            // Print the URL of the uploaded file
            return uploadResult.SecureUrl.ToString();
        }

    }
}
