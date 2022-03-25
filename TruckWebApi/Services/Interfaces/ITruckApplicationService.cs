using TruckWebApi.DTO;
using TruckWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruckWebApi.Services.Interfaces
{
    public interface ITruckApplicationService
    {
        Task<Truck> Post(CreateForm dto);
        Task Delete(int id);
        Task<Truck> Put(int id, CreateForm dto);
        Task<List<Truck>> GetAll(TruckQueryModel query = null);
    }
}
