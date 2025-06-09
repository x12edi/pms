using Microsoft.AspNetCore.Mvc;
using PMS.Application.DTOs;
using PMS.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IHoldingService _holdingService;

        public PortfolioController(IPortfolioService portfolioService, IHoldingService holdingService)
        {
            _portfolioService = portfolioService;
            _holdingService = holdingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortfolioDto>>> GetPortfolios()
        {
            var portfolios = await _portfolioService.GetAllPortfoliosAsync();
            return Ok(portfolios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PortfolioDto>> GetPortfolio(int id)
        {
            var portfolio = await _portfolioService.GetPortfolioByIdAsync(id);
            if (portfolio == null)
                return NotFound();
            return Ok(portfolio);
        }

        [HttpGet("{id}/holdings")]
        public async Task<ActionResult<IEnumerable<HoldingDto>>> GetHoldings(int id)
        {
            var holdings = await _holdingService.GetHoldingsByPortfolioAsync(id);
            return Ok(holdings);
        }
    }
}