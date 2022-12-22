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
            return await context.PointOfInteresets
                                .Where(p => p.CityId == cityId && p.Id == pointOfInterestId)
                                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PointOfIntereset>> GetPointsOfInterestForCityAsync(int cityId)
        {
            return await context.PointOfInteresets.Where(p => p.CityId == cityId).ToListAsync();
        }
    }
}
