namespace CityInfo.API.Services
{
    using CityInfo.API.Entities;

    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();

        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);

        Task<IEnumerable<PointOfIntereset>> GetPointsOfInterestForCityAsync(int cityId);

        Task<PointOfIntereset?> GetPointOfInteresetForCityAsync(int cityId, int pointOfInterestId);
    }
}
