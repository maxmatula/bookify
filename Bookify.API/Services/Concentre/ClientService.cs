using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Bookify.API.Data;
using Bookify.API.DTO.Client;
using Bookify.API.Models;
using Bookify.API.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Bookify.API.Services.Concentre
{
    public class ClientService : IClientService
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;
        public ClientService(DataContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;

        }
        public async Task<bool> Create(ClientForCreateDto client)
        {
            var clientToAdd = _mapper.Map<Client>(client);

            if (clientToAdd != null)
            {
                await _db.Clients.AddAsync(clientToAdd);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var clientToDelete = await _db.Clients.FirstOrDefaultAsync(x => x.ClientId == id);

            if (clientToDelete == null)
            {
                return false;
            }

            _db.Clients.Remove(clientToDelete);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Edit(ClientForEditDto client)
        {
            var objectToEdit = _mapper.Map<Client>(client);
            _db.Clients.Update(objectToEdit);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<ClientDto>> GetAll()
        {
            var clients = await _db.Clients
            .Include(c => c.Bookings)
            .ToListAsync();

            if (clients == null)
            {
                var clientsEmpty = new List<ClientDto>();
                return clientsEmpty;
            }

            var clientsToReturn = _mapper.Map<List<ClientDto>>(clients);

            return clientsToReturn;
        }

        public async Task<ClientDto> GetById(int id)
        {
            var client = await _db.Clients
            .Include(c => c.Bookings)
            .FirstOrDefaultAsync(x => x.ClientId == id);

            var clientToReturn = _mapper.Map<ClientDto>(client);
            return clientToReturn;
        }
    }
}