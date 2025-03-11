using RailwayReservation.Models;

namespace RailwayReservation.Interfaces
{
    public interface IReservationDetail
    {
        Task<IEnumerable<ReservationDetail>> GetAllAsync();
        Task<ReservationDetail> GetByIdAsync(string reservationId);
        Task<ReservationDetail> AddAsync(ReservationDetail reservationDetails);
        Task<ReservationDetail> UpdateAsync(string reservationId, ReservationDetail reservationDetails);
        Task<bool> DeleteAsync(string reservationId);
    }
}
