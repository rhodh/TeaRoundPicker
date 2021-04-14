using AutoMapper;
using Domain.Models;
using Persistence.DBModels;
using System;
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
            var savedEnitity = await _context.DrinkRuns.AddAsync(dbModel);
            await _context.SaveChangesAsync();
            return _mapper.Map<DrinkRun>(savedEnitity.Entity);
        }
    }
}
