using RailwayReservation.Interfaces;
using RailwayReservation.Models;

namespace RailwayReservation.Repositories
{
    public class QueryListRepository : IQueryList
    {
        private OnlineRailwayReservationSystemDbContext context;

        public QueryListRepository(OnlineRailwayReservationSystemDbContext _context)
        {
            context = _context;
        }

        public async Task AddQueryList(QueryList queryList)
        {
            var res = context.QueryLists.FirstOrDefault(q => q.QueryListId == queryList.QueryListId);
            if (res == null)
            {
                context.QueryLists.Add(queryList);
                context.SaveChanges();
            }
        }

        public async Task<QueryList> GetQueryListById(string queryListId)
        {
            var res = context.QueryLists.FirstOrDefault(q => q.QueryListId == queryListId);
            if (res == null)
            {
                return null;
            }

            return res;
        }

        public async Task<IEnumerable<QueryList>> GetQueryListsByQueryId(string queryId)
        {
            var res = context.QueryLists.Where(q => q.QueryId == queryId).ToList();
            if (!res.Any())
            {
                return new List<QueryList>();
            }

            return res;
        }

        public async Task UpdateQueryList(QueryList queryList)
        {
            var res = context.QueryLists.FirstOrDefault(q => q.QueryListId == queryList.QueryListId);
            if (res != null)
            {
                res.QueryDescription = queryList.QueryDescription;
                res.QueryId = queryList.QueryId;
                context.SaveChanges();
            }
        }

        public async Task DeleteQueryList(string queryListId)
        {
            var res = context.QueryLists.FirstOrDefault(q => q.QueryListId == queryListId);
            if (res != null)
            {
                context.QueryLists.Remove(res);
                context.SaveChanges();
            }
        }
    }
}
