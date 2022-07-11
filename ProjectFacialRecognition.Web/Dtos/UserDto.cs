namespace ProjectFacialRecognition.Web.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Birthdate { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string UrlImageRegistration { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}