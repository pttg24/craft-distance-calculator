namespace craft.Services
{
    using System;
    using CSharpFunctionalExtensions;
    using Domain;
    using GeoCoordinatePortable;

    public class DistanceCalculatorService : IDistanceCalculatorService
    {
        public Result<Distance> GetDistance(double sourceLat, double sourceLong, double destLat, double destLong)
        {
            var sourceCoord = new GeoCoordinate(sourceLat, sourceLong);
            var destinationCoord = new GeoCoordinate(destLat, destLong);

            var distanceResult = sourceCoord.GetDistanceTo(destinationCoord);

            Distance finalDistance = new Distance
            {
                Km = Meters2Km(distanceResult),
                Miles = Meters2Miles(distanceResult)
            };

            return Result.Ok<Distance>(finalDistance);
        }

        private Double Meters2Km(double distance) => distance / 1000;
        private Double Meters2Miles(double distance) => distance * 0.000621371192;

    }
}
