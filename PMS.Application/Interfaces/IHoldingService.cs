using PMS.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Application.Interfaces
{
    public interface IHoldingService
    {
        Task<IEnumerable<HoldingDto>> GetHoldingsByPortfolioAsync(int portfolioId);
        Task<HoldingDto> GetHoldingByIdAsync(int id);
        Task<HoldingDto> CreateHoldingAsync(HoldingDto holdingDto);
        Task UpdateHoldingAsync(HoldingDto holdingDto);
        Task DeleteHoldingAsync(int id);
    }
}