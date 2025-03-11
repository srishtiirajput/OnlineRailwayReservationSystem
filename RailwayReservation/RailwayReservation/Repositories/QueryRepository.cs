using RailwayReservation.Interfaces;
using RailwayReservation.Models;

namespace RailwayReservation.Repositories
{
    public class QueryRepository : IQuery
    {
        private OnlineRailwayReservationSystemDbContext context;

        public QueryRepository(OnlineRailwayReservationSystemDbContext _context)
        {
            context = _context;
        }

        public async Task AddQuery(Query query)
        {
            context.Queries.Add(query);
            context.SaveChanges();
        }

        public async Task<Query> GetQueryById(string queryId)
        {
            var res = context.Queries.FirstOrDefault(q => q.QueryId == queryId);
            if (res == null)
            {
                return null;
            }

            return res;
        }

        public async Task<IEnumerable<Query>> GetAllQueries()
        {
            return context.Queries.ToList();
        }

        public async Task<IEnumerable<Query>> GetQueriesByKeyword(string keyword)
        {
            var res = context.Queries.Where(q => q.Keywords.Contains(keyword)).ToList();
            if (!res.Any())
            {
                return new List<Query>();
            }

            return res;
        }
    }
}
