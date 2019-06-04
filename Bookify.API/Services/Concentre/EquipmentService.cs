using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Bookify.API.Data;
using Bookify.API.DTO.Equipment.Return;
using Bookify.API.Models;
using Bookify.API.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Bookify.API.Services.Concentre
{
    public class EquipmentService : IEquipmentService
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;
        public EquipmentService(DataContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;

        }
        public async Task<bool> Create(EquipmentForCreateDto equipment)
        {
            var equipmentToAdd = _mapper.Map<Equipment>(equipment);

            if (equipmentToAdd != null)
            {
                await _db.Equipments.AddAsync(equipmentToAdd);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var equipmentToDelete = await _db.Equipments.FirstOrDefaultAsync(x => x.EquipmentId == id);

            if (equipmentToDelete == null)
            {
                return false;
            }

            _db.Equipments.Remove(equipmentToDelete);
            await _db.SaveChangesAsync();
            return true;

        }

        public async Task<bool> Edit(EquipmentForEditDto equipment)
        {
            var objectToEdit = _mapper.Map<Equipment>(equipment);
            _db.Equipments.Update(objectToEdit);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<EquipmentDto>> GetAll()
        {
            var equipment = await _db.Equipments.ToListAsync();

            if (equipment == null)
            {
                var equipmentEmpty = new List<EquipmentDto>();
                return equipmentEmpty;
            }

            var equipmentToReturn = _mapper.Map<List<EquipmentDto>>(equipment);

            return equipmentToReturn;
        }

        public async Task<EquipmentDto> GetById(int id)
        {
            var equipment = await _db.Equipments.FirstOrDefaultAsync(x => x.EquipmentId == id);

            var equipmentToReturn = _mapper.Map<EquipmentDto>(equipment);
            return equipmentToReturn;
        }
    }
}