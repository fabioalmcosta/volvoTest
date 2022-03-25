using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TruckWebApi.DataAccess
{
    public class Repository : IRepository
    {
        private readonly TruckContext _context;

        public Repository(TruckContext context)
        {
            _context = context;
        }

        public async Task<List<Truck>> GetTruck()
        {
            IQueryable<Truck> trucks = _context.Truck;
            // return await restaurants.OrderByDescending(o => o.Id).ToListAsync();
            return await trucks.OrderBy(o => o.Id).ToListAsync();
        }

        public async Task<Truck> AddTruck(Truck truckEvent)
        {
            await _context.Truck.AddAsync(truckEvent);
            await _context.SaveChangesAsync();

            return truckEvent;
        }

        public async Task<List<Truck>> GetTruck(TruckQueryModel query)
        {
            IQueryable<Truck> trucks = _context.Truck;
            if (query.ModelYear > 0)
            {
                trucks = trucks.Where(o => o.ModelYear == query.ModelYear);
            }

            if (query.Model != null)
            {
                trucks = trucks.Where(o => o.Model == query.Model);
            }

            if (query.NickName != null)
            {
                trucks = trucks.Where(o => o.NickName == query.NickName);
            }

            if (query.ManufacYear > 0)
            {
                trucks = trucks.Where(o => o.ManufacYear == query.ManufacYear);
            }

            if (query.Id > 0)
            {
                trucks = trucks.Where(o => o.Id == query.Id);
            }

            return await trucks.OrderBy(o => o.Id).ToListAsync();
        }

        public async Task<Truck> DeleteTruck(Truck truckEvent)
        {
            _context.Truck.Remove(truckEvent);
            await _context.SaveChangesAsync();

            return truckEvent;
        }

        public async Task<Truck> UpdateTruck(Truck truckEvent)
        {
            _context.Truck.Update(truckEvent);
            await _context.SaveChangesAsync();

            return truckEvent;
        }
    }
}
