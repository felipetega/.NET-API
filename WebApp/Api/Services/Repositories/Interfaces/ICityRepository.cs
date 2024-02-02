using Api.Infrastructure.Models;
using Api.Services.DTOs;

namespace Api.Services.Repositories.Interfaces
{
    public interface ICityRepository
    {
        Task<List<CityDTO>> GetAll();
        Task<CityDTO> GetById(int id);
        Task<CityDTO> Create(CityDTO city);
        Task<CityDTO> Update(CityDTO city, int id);
        Task<bool> Delete(int id);
    }
}
