using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Microsoft.AspNetCore.Mvc;
namespace ProjectFacialRecognition.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RekognitionController : ControllerBase
    {
        private readonly AmazonRekognitionClient _rekognitionClient;
        public RekognitionController(AmazonRekognitionClient rekognitionClient)
        {
            _rekognitionClient = rekognitionClient;
        }
        [HttpGet]
        public async Task<IActionResult> RecognitionFace(string file)
        {

            var request = new DetectFacesRequest();
            var image = new Image();
            var s3Object = new S3Object()
            {
                Bucket = "imagesclass",
                Name = file
            };

            image.S3Object = s3Object;
            request.Image = image;
            request.Attributes = new List<string>() { "ALL" };

            var response = await _rekognitionClient.DetectFacesAsync(request);

            if (response.FaceDetails.Count != 1 && response.FaceDetails.First().Eyeglasses.Value == true)
            {
                BadRequest("No face or face has glasses/sunglasses");
            }

            return Ok(response);

        }
    }
}