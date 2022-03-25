using System.Threading.Tasks;
using System.Collections.Generic;
using TruckWebApi.Models;

namespace TruckWebApi.DataAccess
{
    public interface IRepository
    {
        Task<List<Truck>> GetTruck();
        Task<Truck> AddTruck(Truck truckEvent);
        Task<List<Truck>> GetTruck(TruckQueryModel query);
        Task<Truck> DeleteTruck(Truck truckEvent);
        Task<Truck> UpdateTruck(Truck truckEvent);
    }
}