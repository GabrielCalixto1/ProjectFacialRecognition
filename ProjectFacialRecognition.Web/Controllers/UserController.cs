using Microsoft.AspNetCore.Mvc;
using ProjectFacualRecognition.Lib.Models;

namespace ProjectFacialRecognition.Web.Controllers
{
    [ApiController]
    [Route("UserController")]
    public class UserController : ControllerBase
    {
        private readonly static List<User> Users = new List<User>();
        [HttpPost]
        public async Task<IActionResult> CreateUser(int id, string email, string cpf, string birthdate, string name, string password)
        {
            var users = new User(id, email, cpf, birthdate, name, password, DateTime.Now);
            Users.Add(users);
            return Ok("Successfully created");
        }
        [HttpGet]
        public async Task<IActionResult> GetUserById(int id)
        {
            return Ok(Users.Find(x =>x.Id == id));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateEmailUserById(int id, string email)
        {
            var user = Users.Find(x => x.Id == id);
            user.SetEmail(email);
            return Ok("Successfully changed");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            var user = Users.Find(x => x.Id == id);
            Users.Remove(user);
            return Ok("Successfully deleted");
        }
    }
}