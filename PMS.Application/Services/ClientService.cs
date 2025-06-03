using AutoMapper;
using PMS.Application.DTOs;
using PMS.Application.Interfaces;
using PMS.Domain.Entities;
using PMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientDto>> GetAllClientsAsync()
        {
            var clients = await _unitOfWork.Clients.GetAllAsync();
            return _mapper.Map<IEnumerable<ClientDto>>(clients);
        }

        public async Task<ClientDto> GetClientByIdAsync(int id)
        {
            var client = await _unitOfWork.Clients.GetByIdAsync(id);
            if (client == null)
                return null;
            return _mapper.Map<ClientDto>(client);
        }

        public async Task<ClientDto> CreateClientAsync(ClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);
            client.CreatedAt = DateTime.UtcNow;
            await _unitOfWork.Clients.AddAsync(client);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ClientDto>(client);
        }

        public async Task UpdateClientAsync(ClientDto clientDto)
        {
            var client = await _unitOfWork.Clients.GetByIdAsync(clientDto.Id);
            if (client == null)
                throw new KeyNotFoundException($"Client with ID {clientDto.Id} not found.");
            _mapper.Map(clientDto, client);
            await _unitOfWork.Clients.UpdateAsync(client);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteClientAsync(int id)
        {
            var client = await _unitOfWork.Clients.GetByIdAsync(id);
            if (client == null)
                throw new KeyNotFoundException($"Client with ID {id} not found.");
            await _unitOfWork.Clients.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}