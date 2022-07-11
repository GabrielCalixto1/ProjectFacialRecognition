using ProjectFacualRecognition.Lib.Models;

namespace ProjectFacualRecognition.Lib.Data.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : ModelBase
    {
        Task CreateUser(T user);
        Task<T> GetUserById(int id);
        Task<List<T>> GetAllUsers();
        Task DeleteUserById(int id);

    }
}