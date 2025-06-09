using Microsoft.AspNetCore.Mvc;
using PMS.Application.Interfaces;
using PMS.Application.Services;

namespace PMS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoldingController : ControllerBase
    {
        private readonly IHoldingService _holdingService;

        public HoldingController(IHoldingService holdingService)
        {
            _holdingService = holdingService;
        }

        [HttpGet("portfolio/{portfolioId}")]
        public async Task<IActionResult> GetHoldingsByPortfolio(int portfolioId)
        {
            var holdings = await _holdingService.GetHoldingsByPortfolioAsync(portfolioId);
            return Ok(holdings);
        }
    }
}
