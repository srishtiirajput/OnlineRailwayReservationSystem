using Microsoft.EntityFrameworkCore;
using RailwayReservation.Interfaces;
using RailwayReservation.Models;

namespace RailwayReservation.Repositories
{
    public class SeatRepository
    {
        private OnlineRailwayReservationSystemDbContext context;

        public SeatRepository(OnlineRailwayReservationSystemDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<Seat>> GetAllAsync()
        {
            return context.Seats.ToList();
        }

        //public async Task<Seat> GetByIdAsync(string seatId)
        //{
        //    return context.Seats.FirstOrDefault(s => s.SeatId == seatId);
        //}
        //public async Task<IEnumerable<Seat>> GetByCoachIdAsync(string coachId)
        //{
        //    return context.Seats.Where(s => s.CoachId == coachId).ToList();
        //}

        //Update the availability status of a seat asynchronously
        //public async Task UpdateAvailabilityStatusAsync(string seatId, bool availabilityStatus)
        //{
        //    //var seat = context.Seats.FirstOrDefault(s => s.SeatId == seatId);
        //    if (seat != null)
        //    {
        //        seat.AvailabilityStatus = availabilityStatus;
        //        context.SaveChanges();
        //    }
        //}
    }
}
