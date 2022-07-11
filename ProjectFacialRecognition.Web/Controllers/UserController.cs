using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProjectFacialRecognition.Web.Dtos;
using ProjectFacualRecognition.Lib.Data.Repositories.Interfaces;
using ProjectFacualRecognition.Lib.Models;

namespace ProjectFacialRecognition.Web.Controllers
{
    [ApiController]
    [Route("UserController")]
    public class UserController : ControllerBase
    {
        private readonly static List<User> Users = new List<User>();
        private readonly IUserRepository _repository;
        [HttpPost]
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
        [HttpGet]
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
        [HttpPut]
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
        [HttpDelete]
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