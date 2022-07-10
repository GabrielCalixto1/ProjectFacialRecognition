using Amazon.S3;
using Microsoft.AspNetCore.Mvc;

namespace ProjectFacialRecognition.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IAmazonS3 _amazonS3;
        private readonly List<string> _extensionsImage =
        new List<string>(){"image/jpeg", "image/png"};
        public ImagesController(IAmazonS3 amazonS3)
        {
            _amazonS3 = amazonS3;
        }
        [HttpPost]
        public async Task<IActionResult> CreateImage(IFormFile image)
        {
            if(!_extensionsImage.Contains(image.ContentType))
            {
                return BadRequest("Tipo Inválido.");
            }
            return Ok(); 
        }
   
        [HttpGet("bucket")]
        public async Task<IActionResult> ListBuckets()
        {
            var response = await _amazonS3.ListBucketsAsync();
            return Ok(response);
        }
        [HttpPost("bucket")] 
        public async Task<IActionResult> CreateBucket(string nameBucket)
        {
            var response = await _amazonS3.PutBucketAsync(nameBucket);
            return Ok(response);
        }


    }
}