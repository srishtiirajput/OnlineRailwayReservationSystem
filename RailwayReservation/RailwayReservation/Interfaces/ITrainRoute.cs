using RailwayReservation.Models;

namespace RailwayReservation.Interfaces
{
    public interface ITrainRoute
    {
        Task<bool> AddRoute(TrainRoute route);
        Task<IEnumerable<TrainRoute>> GetAllRoutes();
        Task<IEnumerable<TrainRoute>> GetRoutesBySource(string source);
        Task<IEnumerable<TrainRoute>> GetRoutesByDestination(string destination);
        Task<IEnumerable<TrainRoute>> GetRoutesBetweenStations(string source, string destination);
    }
}
