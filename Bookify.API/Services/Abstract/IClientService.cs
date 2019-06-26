using System.Collections.Generic;
using System.Threading.Tasks;
using Bookify.API.DTO.Client;

namespace Bookify.API.Services.Abstract
{
    public interface IClientService
    {
         Task<List<ClientDto>> GetAll(); 
         Task<ClientDto> GetById(int id);
         Task<bool> Create(ClientForCreateDto client);
         Task<bool> Edit(ClientForEditDto client);
         Task<bool> Delete(int id);
    }
}