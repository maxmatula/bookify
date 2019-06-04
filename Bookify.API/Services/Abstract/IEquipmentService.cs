using System.Collections.Generic;
using System.Threading.Tasks;
using Bookify.API.DTO.Equipment.Return;

namespace Bookify.API.Services.Abstract
{
    public interface IEquipmentService
    {
         Task<List<EquipmentDto>> GetAll(); 
         Task<EquipmentDto> GetById(int id);
         Task<bool> Create(EquipmentForCreateDto equipment);
         Task<bool> Edit(EquipmentForEditDto equipment);
         Task<bool> Delete(int id);
    }
}