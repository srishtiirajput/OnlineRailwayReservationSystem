using System.ComponentModel.DataAnnotations;
using RailwayReservation.Models;

namespace RailwayReservation.Interfaces
{
    public interface ITrain
    {
        Task<IEnumerable<Train>> GetAllTrainsAsync();
        Task<Train> GetTrainByIdAsync(string trainId);
        Task<Train> GetTrainByNameAsync(string trainName);

        Task AddTrainAsync(Train train);

        Task UpdateTrainAsync(string trainId, Train train);

        Task DeleteTrainAsync(string trainId);

        Task<IEnumerable<Train>> GetTrainsByRouteAsync(string trainRoute);

        Task<IEnumerable<Train>> GetTrainsByRunningDayAsync(string runningDay);
        void ValidateTrainId(string trainId);

        Task ValidateTrainAsync(Train train);

    }
}
