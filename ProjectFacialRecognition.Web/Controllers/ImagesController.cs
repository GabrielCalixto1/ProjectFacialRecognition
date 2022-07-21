using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;

namespace ProjectFacialRecognition.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IAmazonS3 _amazonS3;
        private readonly List<string> _extensionsImage =
        new List<string>() { "image/jpeg", "image/png" };
        public ImagesController(IAmazonS3 amazonS3)
        {
            _amazonS3 = amazonS3;
        }
        [HttpPost("1")]
        public async Task<IActionResult> CreateImage(IFormFile image)
        {
            if (!_extensionsImage.Contains(image.ContentType))
            {
                return BadRequest("Invalid Type.");
            }
            using (var imageStream = new MemoryStream())
            {
                await image.CopyToAsync(imageStream);

                var request = new PutObjectRequest();
                request.Key = "selfie " + image.FileName;
                request.BucketName = "imagesclass";
                request.InputStream = imageStream;
                var response = await _amazonS3.PutObjectAsync(request);
                return Ok(response);

            }
        }
        [HttpDelete("2")]
        public async Task<IActionResult> DeleteImage(string nameFileInS3)
        {
            var response = await _amazonS3.DeleteObjectAsync("imagesclass", nameFileInS3);
            return Ok(response);
        }

        [HttpGet("3")]
        public async Task<IActionResult> ListBuckets()
        {
            var response = await _amazonS3.ListBucketsAsync();
            return Ok(response);
        }
        [HttpPost("Bucket")]
        public async Task<IActionResult> CreateBucket(string nameBucket)
        {
            var response = await _amazonS3.PutBucketAsync(nameBucket);
            return Ok(response);
        }


    }
}