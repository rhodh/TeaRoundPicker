using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.DBModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.DrinkRunRepo
{
    public class DrinkRunRepository : IDrinkRunWriter
    {
        private readonly TeaRoundPickerContext _context;
        private readonly IMapper _mapper;

        public DrinkRunRepository(TeaRoundPickerContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<DrinkRun> CreateDrinkRun(DrinkRun drinkRun)
        {
            var dbModel = _mapper.Map<DrinkRunDbModel>(drinkRun);
            var orders = drinkRun.Orders.Select(x => x.Id);
            dbModel.DrinkOrders = await _context.DrinkOrders.Where(x => orders.Contains(x.Id)).ToListAsync();
            var savedEnitity = await _context.DrinkRuns.AddAsync(dbModel);
            await _context.SaveChangesAsync();
            return _mapper.Map<DrinkRun>(savedEnitity.Entity);
        }
    }
}
