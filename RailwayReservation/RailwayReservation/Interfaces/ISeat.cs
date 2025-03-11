using RailwayReservation.Models;

namespace RailwayReservation.Interfaces
{
    public interface ISeat
    {
        Task<IEnumerable<Seat>> GetAllAsync();
        Task<Seat> GetByIdAsync(string seatId);
        Task<IEnumerable<Seat>> GetByCoachIdAsync(string coachId);
        Task UpdateAvailabilityStatusAsync(string seatId, bool availabilityStatus);
    }
}
