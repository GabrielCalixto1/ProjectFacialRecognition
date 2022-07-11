using ProjectFacualRecognition.Lib.Models;

namespace ProjectFacualRecognition.Lib.Data.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task UpdateEmailUserById(int id, string email);
    }
}