using Api.Infrastructure.Models;

namespace Api.Infrastructure.Repositories.Interfaces
{
    public interface ICityRepository
    {
        Task<List<City>> GetAll();
        Task<City> GetById(int id);
        Task<City> Create(City city);
        Task<City> Update(City city, int id);
        Task<bool> Delete(int id);
    }
}
