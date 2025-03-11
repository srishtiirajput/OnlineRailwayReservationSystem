using Microsoft.EntityFrameworkCore;
using RailwayReservation.Interfaces;
using RailwayReservation.Models;

namespace RailwayReservation.Repositories
{
    public class ReservationDetailRepository : IReservationDetail
    {
        private readonly OnlineRailwayReservationSystemDbContext context;

        public ReservationDetailRepository(OnlineRailwayReservationSystemDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<ReservationDetail>> GetAllAsync()
        {
            return context.ReservationDetails.Include(r => r.Payment).Include(r => r.Ticket).Include(r => r.User).ToList();
        }

        public async Task<ReservationDetail> GetByIdAsync(string reservationId)
        {
            var res = context.ReservationDetails
                .Include(r => r.Payment)
                .Include(r => r.Ticket)
                .Include(r => r.User)
                .FirstOrDefault(r => r.ReservationId == reservationId);

            if (res == null)
            {
                return null;
            }

            return res;
        }

        public async Task<ReservationDetail> AddAsync(ReservationDetail reservationDetail)
        {
            context.ReservationDetails.Add(reservationDetail);
            context.SaveChanges();
            return reservationDetail;
        }

        public async Task<ReservationDetail> UpdateAsync(string reservationId, ReservationDetail reservationDetail)
        {
            var existingReservation = context.ReservationDetails.Find(reservationId);
            if (existingReservation == null) return null;

            existingReservation.Name = reservationDetail.Name;
            existingReservation.Gender = reservationDetail.Gender;
            existingReservation.Age = reservationDetail.Age;
            existingReservation.Address = reservationDetail.Address;
            existingReservation.PaymentId = reservationDetail.PaymentId;
            existingReservation.TicketId = reservationDetail.TicketId;
            existingReservation.UserId = reservationDetail.UserId;

            context.ReservationDetails.Update(existingReservation);
            context.SaveChanges();
            return existingReservation;
        }

        public async Task<bool> DeleteAsync(string reservationId)
        {
            var reservation = context.ReservationDetails.Find(reservationId);
            if (reservation == null) return false;

            context.ReservationDetails.Remove(reservation);
            context.SaveChanges();
            return true;
        }
    }
}
