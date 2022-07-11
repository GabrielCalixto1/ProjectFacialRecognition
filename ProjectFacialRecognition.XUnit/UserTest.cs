using ProjectFacualRecognition.Lib.Models;

namespace ProjectFacialRecognition.XUnit
{
    public class UserTest
    {

        [Fact]
        public async Task TestEmailInTheUserClass()
        {
            var expectedEmail = "test@test.com";
            var user = new User(1, expectedEmail, "12345678911", DateTime.Parse("01/01/2000"), "joao", "123456789", DateTime.Now);
            var currentEmail = user.Email;
            Assert.Equal(expectedEmail, currentEmail);
        }
        [Fact]
        public async Task TestCpfInTheUserClasss()
        {
            var expectedCpf = "12345678911";
            var user = new User(1, "test@test.com", expectedCpf, DateTime.Parse("01/01/2000"), "joao", "123456789", DateTime.Now);
            var currentCpf = user.Cpf;
            Assert.Equal(expectedCpf, currentCpf);
        }
        [Fact]
        public async Task TestBirthdateInTheUserClass()
        {
            var expectedBirthdate = DateTime.Parse("01/01/2000");
            var user = new User(1, "test@test.com", "12345678911", expectedBirthdate, "joao", "123456789", DateTime.Now);
            var currentBirthdate = user.Birthdate;
            Assert.Equal(expectedBirthdate, currentBirthdate);
        }
        [Fact]
        public async Task TestPasswordInTheUserClass()
        {
            var expectedPassword = "123456789";
            var user = new User(1, "test@test.com", "12345678911", DateTime.Parse("01/01/2000"), "joao", expectedPassword, DateTime.Now);
            var currentPassword = user.Password;
            Assert.Equal(expectedPassword, currentPassword);
        }
    }
}