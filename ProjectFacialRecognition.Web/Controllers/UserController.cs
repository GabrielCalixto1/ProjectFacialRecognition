using System.ComponentModel.DataAnnotations;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;
using ProjectFacialRecognition.Web.Dtos;
using ProjectFacualRecognition.Lib.Data.Repositories;
using ProjectFacualRecognition.Lib.Data.Repositories.Interfaces;
using ProjectFacualRecognition.Lib.Models;

namespace ProjectFacialRecognition.Web.Controllers
{
    [ApiController]
    [Route("UserController")]
    public class UserController : ControllerBase
    {
        private readonly AmazonRekognitionClient _rekognitionClient;
        private readonly IAmazonS3 _amazonS3;
        private readonly List<string> _extensionsImage =
        new List<string>() { "image/jpeg", "image/png" };
        private readonly static List<User> Users = new List<User>();
        private readonly IUserRepository _repository;
        public UserController(IUserRepository repository, IAmazonS3 amazonS3, AmazonRekognitionClient rekognitionClient)
        {
            _rekognitionClient = rekognitionClient;
            _amazonS3 = amazonS3;
            _repository = repository;
        }
        [HttpPost("LoginImage")]
        public async Task<IActionResult> LoginImage(int id, IFormFile imageLogin)
        {
            var user = await _repository.GetUserById(id);
            var urlImage = user.UrlImageRegistration;
            var comparation = await CompareFace(urlImage, imageLogin);
            if (!comparation)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("Image")]
        public async Task<IActionResult> RegisterImage(int id, IFormFile image)
        {
            var fileName = await SaveS3(image);
            var imageValid = await ValidateImage(fileName);
            if (imageValid)
            {
                //update postgree to add url image in user
                return Ok();
            }
            else
            {

                await _amazonS3.DeleteObjectAsync("imagesclass", fileName);
                return BadRequest();
            }
        }
        [HttpGet("Login")]
        public async Task<IActionResult> LoginValidate(string email, string password)
        {
            var user = await _repository.GetUserByEmail(email);
            if (user.Password != password)
            {
                return BadRequest();
            }

            return Ok(user.Id);
        }
        public async Task<bool> ValidateImage(string file)
        {

            var request = new DetectFacesRequest();
            var image = new Image();
            var s3Object = new Amazon.Rekognition.Model.S3Object()
            {
                Bucket = "imagesclass",
                Name = file
            };

            image.S3Object = s3Object;
            request.Image = image;
            request.Attributes = new List<string>() { "ALL" };

            var response = await _rekognitionClient.DetectFacesAsync(request);

            if (response.FaceDetails.Count() != 1 || response.FaceDetails.First().Eyeglasses.Value == true)
            {
                return false;
            }

            return true;

        }
        public async Task<string> SaveS3(IFormFile image)
        {
            if (!_extensionsImage.Contains(image.ContentType))
            {
                throw new Exception("Invalid Type.");
            }
            using (var imageStream = new MemoryStream())
            {
                await image.CopyToAsync(imageStream);

                var request = new PutObjectRequest();
                request.Key = "selfie " + image.FileName;
                request.BucketName = "imagesclass";
                request.InputStream = imageStream;
                var response = await _amazonS3.PutObjectAsync(request);
                return request.Key;

            }
        }
        public async Task<bool> CompareFace(string nameFileS3, IFormFile selfieLogin)
        {
            using (var memoryStream = new MemoryStream())
            {
                var request = new CompareFacesRequest();
                var requestSource = new Image()
                {
                    S3Object = new Amazon.Rekognition.Model.S3Object()
                    {
                        Bucket = "imagesclass",
                        Name = nameFileS3
                    }
                };
                await selfieLogin.CopyToAsync(memoryStream);
                var requestTarget = new Image()
                {
                    Bytes = memoryStream
                };
                request.SourceImage = requestSource;
                request.TargetImage = requestTarget;

                var compareResult = await _rekognitionClient.CompareFacesAsync(request);
                var similarity = compareResult.FaceMatches.First(x => x.Similarity >= 0);

                if (similarity.Similarity < 60)
                {
                    return false;
                }

                return true;
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            try
            {
                var users = new User(userDto.Id, userDto.Email, userDto.Cpf, userDto.Birthdate, userDto.Name, userDto.Password, userDto.RegistrationDate);
                await _repository.CreateUser(users);
                return Ok("Successfully created");
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet()]
        public async Task<IActionResult> GetUserById(int id)
        {

            try
            {
                return Ok(await _repository.GetUserById(id));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAllUsers()
        {

            try
            {
                return Ok(await _repository.GetAllUsers());
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut()]
        public async Task<IActionResult> UpdateEmailUserById(int id, string email)
        {

            try
            {
                await _repository.UpdateEmailUserById(id, email);
                return Ok("Successfully changed");
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete()]
        public async Task<IActionResult> DeleteUserById(int id)
        {

            try
            {
                await _repository.DeleteUserById(id);
                return Ok("Successfully deleted");
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}