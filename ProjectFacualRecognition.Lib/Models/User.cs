using ProjectFacualRecognition.Lib.Exceptions;
using System;
namespace ProjectFacualRecognition.Lib.Models
{
    public class User
    {
        public int Id { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public DateTime Birthday { get; private set; }
        public string Name { get; private set; }
        public string Password { get; private set; }
        public string UrlImageRegistration { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public User(int id, string email, string cpf, string birthdate, string name, string password, DateTime registrationDate)
        {
            SetId(id);
            SetEmail(email);
            SetCpf(cpf);
            SetBirthday(birthdate);
            SetName(name);
            SetPassword(password);
            SetRegistrationDate(registrationDate);
        }
        public void SetId(int id)
        {
            Id = id;
        }
        public void SetEmail(string email)
        {
            if (email.Contains('@'))
            {
                Email = email;
            }
            throw new ValidationException("Email must contain @");
        }
        public void SetCpf(string cpf)
        {
            if (cpf.Length <= 11 && CpfMustHaveOnlyNumbers(cpf))
            {
                Cpf = cpf;
            }
        }
        public bool CpfMustHaveOnlyNumbers(string cpf)
        {
            if (cpf.Where(x => char.IsLetter(x)).Count() > 0)
            {
                return false;
            }
            throw new ValidationException("Cpf must have only numbers");
        }

        public void SetBirthday(string birthday)
        {

            if (DateTime.Parse(birthday) < DateTime.Parse("01/01/2010"))
            {
                Birthday = DateTime.Parse(birthday);
            }
            throw new ValidationException("Birthdate must be less than 2010");

        }
        public void SetName(string name)
        {
            Name = name;
        }
        public void SetPassword(string password)
        {
            if (password.Length >= 8)
            {
                Password = password;
            }
            throw new ValidationException("Password must be at least 8 digits");

        }
        public void SetUrlImageRegistration(string urlImageRegistration)
        {
            UrlImageRegistration = urlImageRegistration;
        }
        public void SetRegistrationDate(DateTime registrationDate)
        {
            RegistrationDate = registrationDate;
        }
    }
}