using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Bookify.API.Data;
using Bookify.API.DTO.House;
using Bookify.API.Models;
using Bookify.API.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Bookify.API.Services.Concentre
{
    public class HouseService : IHouseService
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;
        public HouseService(DataContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;

        }

        public async Task<bool> Create(HouseForCreateDto house)
        {
            var houseToAdd = _mapper.Map<House>(house);

            if (houseToAdd != null)
            {
                await _db.Houses.AddAsync(houseToAdd);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var houseToDelete = await _db.Houses.FirstOrDefaultAsync(x => x.HouseId == id);

            if (houseToDelete == null)
            {
                return false;
            }

            _db.Houses.Remove(houseToDelete);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Edit(HouseForEditDto house)
        {
            var objectToEdit = _mapper.Map<House>(house);
            _db.Houses.Update(objectToEdit);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<HouseDto>> GetAll()
        {
            var houses = await _db.Houses
                .Include(h => h.Equipment)
                .Include(h => h.Bookings)
                .ToListAsync();

            if (houses == null)
            {
                var housesEmpty = new List<HouseDto>();
                return housesEmpty;
            }

            var housesToReturn = _mapper.Map<List<HouseDto>>(houses);
            return housesToReturn;
        }

        public async Task<HouseDto> GetById(int id)
        {
            var house = await _db.Houses
                .Include(h => h.Equipment)
                .Include(h => h.Bookings)
                .FirstOrDefaultAsync(x => x.HouseId == id);

            var houseToReturn = _mapper.Map<HouseDto>(house);
            return houseToReturn;
        }
    }
}