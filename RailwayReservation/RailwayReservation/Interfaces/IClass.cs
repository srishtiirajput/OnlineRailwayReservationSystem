using RailwayReservation.Models;

namespace RailwayReservation.Interfaces
{
    public interface IClass
    {
        Task<IEnumerable<Class>> GetAll();
        Task<IEnumerable<Class>> SearchByClassName(string className);
        Task<IEnumerable<Class>> GetByClassType(string classType);
    }
}
