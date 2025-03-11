using Microsoft.EntityFrameworkCore;
using RailwayReservation.Repositories;
using RailwayReservation.Interfaces;
using RailwayReservation.Models;

namespace RailwayReservation.Repositories
{
    public class TrainRouteRepository : ITrainRoute
    {
        private readonly OnlineRailwayReservationSystemDbContext _context;
        public TrainRouteRepository(OnlineRailwayReservationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddRoute(TrainRoute trainRoute)
        {
            if(trainRoute == null)
            {
                return false;
            }

            if(string.IsNullOrEmpty(trainRoute.Source) || string.IsNullOrEmpty(trainRoute.Destination))
            {
                return false;
            }

            bool routeExists = _context.TrainRoutes.Any(r => r.Source == trainRoute.Source && r.Destination == trainRoute.Destination && r.RouteId == trainRoute.RouteId);

            if(routeExists)
            {
                return false;
            }
            try
            {
                _context.TrainRoutes.Add(trainRoute);
                _context.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<TrainRoute>> GetAllRoutes()
        {
            return _context.TrainRoutes.ToList();
        }

        public async Task<IEnumerable<TrainRoute>> GetRoutesBySource(string source)
        {
            return _context.TrainRoutes.Where(r => r.Source == source).ToList();
        }

        public async Task<IEnumerable<TrainRoute>> GetRoutesByDestination(string destination)
        {
            return _context.TrainRoutes.Where(r => r.Destination == destination).ToList();
        }

        public async Task<IEnumerable<TrainRoute>> GetRoutesBetweenStations(string source, string destination)
        {
            return _context.TrainRoutes.Where(r => r.Source == source && r.Destination == destination).ToList();
        }


    }
}
