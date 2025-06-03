using PMS.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Application.Interfaces
{
    public interface IPortfolioService
    {
        Task<IEnumerable<PortfolioDto>> GetAllPortfoliosAsync();
        Task<PortfolioDto> GetPortfolioByIdAsync(int id);
        Task<PortfolioDto> CreatePortfolioAsync(PortfolioDto portfolioDto);
        Task UpdatePortfolioAsync(PortfolioDto portfolioDto);
        Task DeletePortfolioAsync(int id);
    }
}