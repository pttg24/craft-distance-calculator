namespace craft.Services
{
    using CSharpFunctionalExtensions;
    using Domain;

    public interface IDistanceCalculatorService
    {
        Result<Distance> GetDistance(double sourceLat, double sourceLong, double destLat, double destLong);
    }
}
