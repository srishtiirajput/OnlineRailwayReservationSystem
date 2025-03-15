using Microsoft.AspNetCore.Mvc;
using RailwayReservation.Models;

namespace RailwayReservation.Interfaces
{
    public interface ISupport
    {
        Task<IEnumerable<Query>> GetAllQueries();
        Task<Support> CreateSupportAsync(string userQuery, string userId);
        Task<Query> GetQueryByKeywordAsync(string userQuery);
        Task<List<QueryList>> GetQueryListsByDescriptionAsync(string userQuery);
        Task<Support> GetSupportResponseAsync(string userQuery);
        Task<Support> UpdateSupportAsync(string supportId, string newQueryText);
        Task<bool> DeleteSupportAsync(string supportId);


    }
}
