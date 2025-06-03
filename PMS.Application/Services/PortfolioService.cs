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
    public class PortfolioService : IPortfolioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PortfolioService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PortfolioDto>> GetAllPortfoliosAsync()
        {
            var portfolios = await _unitOfWork.Portfolios.GetAllAsync();
            return _mapper.Map<IEnumerable<PortfolioDto>>(portfolios);
        }

        public async Task<PortfolioDto> GetPortfolioByIdAsync(int id)
        {
            var portfolio = await _unitOfWork.Portfolios.GetByIdAsync(id);
            if (portfolio == null)
                return null;
            return _mapper.Map<PortfolioDto>(portfolio);
        }

        public async Task<PortfolioDto> CreatePortfolioAsync(PortfolioDto portfolioDto)
        {
            var portfolio = _mapper.Map<Portfolio>(portfolioDto);
            portfolio.CreatedAt = DateTime.UtcNow;
            await _unitOfWork.Portfolios.AddAsync(portfolio);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<PortfolioDto>(portfolio);
        }

        public async Task UpdatePortfolioAsync(PortfolioDto portfolioDto)
        {
            var portfolio = await _unitOfWork.Portfolios.GetByIdAsync(portfolioDto.Id);
            if (portfolio == null)
                throw new KeyNotFoundException($"Portfolio with ID {portfolioDto.Id} not found.");
            _mapper.Map(portfolioDto, portfolio);
            await _unitOfWork.Portfolios.UpdateAsync(portfolio);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeletePortfolioAsync(int id)
        {
            var portfolio = await _unitOfWork.Portfolios.GetByIdAsync(id);
            if (portfolio == null)
                throw new KeyNotFoundException($"Portfolio with ID {id} not found.");
            await _unitOfWork.Portfolios.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}