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
    public class HoldingService : IHoldingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HoldingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HoldingDto>> GetHoldingsByPortfolioAsync(int portfolioId)
        {
            var holdings = await _unitOfWork.Holdings.FindAsync(h => h.PortfolioId == portfolioId);
            return _mapper.Map<IEnumerable<HoldingDto>>(holdings);
        }

        public async Task<HoldingDto> GetHoldingByIdAsync(int id)
        {
            var holding = await _unitOfWork.Holdings.GetByIdAsync(id);
            if (holding == null)
                return null;
            return _mapper.Map<HoldingDto>(holding);
        }

        public async Task<HoldingDto> CreateHoldingAsync(HoldingDto holdingDto)
        {
            var holding = _mapper.Map<Holding>(holdingDto);
            await _unitOfWork.Holdings.AddAsync(holding);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<HoldingDto>(holding);
        }

        public async Task UpdateHoldingAsync(HoldingDto holdingDto)
        {
            var holding = await _unitOfWork.Holdings.GetByIdAsync(holdingDto.Id);
            if (holding == null)
                throw new KeyNotFoundException($"Holding with ID {holdingDto.Id} not found.");
            _mapper.Map(holdingDto, holding);
            await _unitOfWork.Holdings.UpdateAsync(holding);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteHoldingAsync(int id)
        {
            var holding = await _unitOfWork.Holdings.GetByIdAsync(id);
            if (holding == null)
                throw new KeyNotFoundException($"Holding with ID {id} not found.");
            await _unitOfWork.Holdings.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}