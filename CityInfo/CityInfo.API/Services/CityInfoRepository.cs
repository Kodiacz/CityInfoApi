using CityInfo.API.Data;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoDbContext context;

        public CityInfoRepository(CityInfoDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await context.Cities.ToListAsync();
        }

        public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
        {

            if (includePointsOfInterest)
            {
                return await context.Cities.Include(x => x.PointOfInteresets)
                    .Where(c => c.Id == cityId)
                    .FirstOrDefaultAsync();
            }

            return await context.Cities
                                .Where(c => c.Id == cityId)
                                .FirstOrDefaultAsync();
        }

        public async Task<PointOfIntereset?> GetPointOfInteresetForCityAsync(int cityId, int pointOfInterestId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PointOfIntereset>> GetPointsOfInterestForCityAsync(int cityId)
        {
            throw new NotImplementedException();
        }
    }
}
