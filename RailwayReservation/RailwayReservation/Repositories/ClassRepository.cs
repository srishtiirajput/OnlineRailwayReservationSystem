using RailwayReservation.Interfaces;
using RailwayReservation.Models;

namespace RailwayReservation.Repositories
{
    public class ClassRepository : IClass
    {
        private readonly OnlineRailwayReservationSystemDbContext _context;
        public ClassRepository(OnlineRailwayReservationSystemDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Class>> GetAll()
        {
            return _context.Classes.ToList();
        }

        //public Class? GetById(string classId)
        //{
        //    return _context.Classes.FirstOrDefault(c => c.ClassId == classId);
        //}

        public async Task<IEnumerable<Class>> SearchByClassName(string className)
        {
            return _context.Classes.Where(c => c.ClassName == className).ToList();
        }

        public async Task<IEnumerable<Class>> GetByClassType(string classType)
        {
            return _context.Classes.Where(c => c.ClassType == classType).ToList();
        }
    }
}
