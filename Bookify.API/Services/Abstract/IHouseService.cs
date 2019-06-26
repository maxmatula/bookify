using System.Collections.Generic;
using System.Threading.Tasks;
using Bookify.API.DTO.House;

namespace Bookify.API.Services.Abstract
{
    public interface IHouseService
    {
         Task<List<HouseDto>> GetAll(); 
         Task<HouseDto> GetById(int id);
         Task<bool> Create(HouseForCreateDto house);
         Task<bool> Edit(HouseForEditDto house);
         Task<bool> Delete(int id);
    }
}