using TruckWebApi.DataAccess;
using TruckWebApi.DTO;
using TruckWebApi.Exceptions;
using TruckWebApi.Models;
using TruckWebApi.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System;

namespace TruckWebApi.Services
{
    public class TruckApplicationService : ITruckApplicationService
    {
        private IRepository _repo;
        public TruckApplicationService(IRepository repo)
        {
            _repo = repo;
        }

        public async Task<Truck> Post(CreateForm dto)
        {
            ValidateForm(dto);

            var newTruck = new Truck()
            {
                ManufacYear = dto.ManufacYear,
                Model = dto.Model,
                ModelYear = dto.ModelYear,
                NickName = dto.NickName,
            };
            
            await _repo.AddTruck(newTruck);

            return newTruck;
        }

        private void ValidateForm(CreateForm dto)
        {
            if (dto.Model != "FH" && dto.Model != "FM")
                throw new HttpStatusException(HttpStatusCode.BadRequest, "The truck model is not a valid model.");

            if (!Enumerable.Range(1900, 3000).Contains(dto.ModelYear))
                throw new HttpStatusException(HttpStatusCode.BadRequest, "The truck model year is not a valid year.");

            if (!Enumerable.Range(1900, 3000).Contains(dto.ManufacYear))
                throw new HttpStatusException(HttpStatusCode.BadRequest, "The truck manufacture year is not a valid year.");
        }

        public async Task Delete(int id)
        {
            var query = new TruckQueryModel()
            {
                Id = id
            };
            var truckList = await _repo.GetTruck(query);

            if (!truckList.Any()) throw new HttpStatusException(HttpStatusCode.NotFound, "Truck not found.");
                
            await _repo.DeleteTruck(truckList.FirstOrDefault());
        }

        public async Task<Truck> Put(int id, CreateForm dto)
        {
            ValidateForm(dto);

            var query = new TruckQueryModel()
            {
                Id = id
            };
            var truckList = await _repo.GetTruck(query);

            if (!truckList.Any()) throw new HttpStatusException(HttpStatusCode.NotFound, "Truck not found.");

            var truck = truckList.FirstOrDefault();
            truck.NickName = dto.NickName;
            truck.ManufacYear = dto.ManufacYear;
            truck.ModelYear = dto.ModelYear;
            truck.Model = dto.Model;

            await _repo.UpdateTruck(truck);

            return truck;
        }

        public async Task<List<Truck>> GetAll(TruckQueryModel query = null)
        {
            var truckList = new List<Truck>();

            if (query != null)
            {
                truckList = await _repo.GetTruck(query);
            }
            else
            {
                truckList = await _repo.GetTruck();
            }

            return truckList;
        }
    }
}
