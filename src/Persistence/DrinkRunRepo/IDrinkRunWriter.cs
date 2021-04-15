using Domain.Models;
using System.Threading.Tasks;

namespace Persistence.DrinkRunRepo
{
    public interface IDrinkRunWriter
    {
        Task<DrinkRun> CreateDrinkRun(DrinkRun drinkRun);
    }
}
