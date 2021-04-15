using Domain.Dto;
using Domain.Models;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IDrinkRunService
    {
        Task<DrinkRun> CreateDrinkRun(DrinkRunDto drinkRunDto);
    }
}
