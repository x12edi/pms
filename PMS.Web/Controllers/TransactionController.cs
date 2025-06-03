using Microsoft.AspNetCore.Mvc;
using PMS.Application.DTOs;
using PMS.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("holding/{holdingId}")]
        public async Task<IActionResult> GetTransactionsByHolding(
            int holdingId,
            [FromQuery] int draw, // DataTables draw counter
            [FromQuery] int start, // Starting record index
            [FromQuery] int length, // Page size
            [FromQuery] string? searchValue, // Search term
            [FromQuery] string? orderColumn, // Column to sort (index)
            [FromQuery] string? orderDir // Sort direction (asc/desc)
        )
        {
            var transactions = await _transactionService.GetTransactionsByHoldingAsync(holdingId);

            // Apply search
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = searchValue.ToLower();
                transactions = transactions.Where(t => t.Type.ToLower().Contains(searchValue)).ToList();
            }

            // Get total records
            var totalRecords = transactions.Count();
            var filteredRecords = totalRecords;

            // Map DataTables column index to property
            var columnMap = new Dictionary<int, string>
            {
                { 0, "id" },
                { 1, "type" },
                { 2, "quantity" },
                { 3, "price" },
                { 4, "amount" },
                { 5, "date" },
                { 6, "fees" }
            };

            // Apply sorting
            if (!string.IsNullOrEmpty(orderColumn) && int.TryParse(orderColumn, out int columnIndex) && columnMap.ContainsKey(columnIndex))
            {
                var sortBy = columnMap[columnIndex];
                transactions = sortBy switch
                {
                    "id" => orderDir.ToLower() == "asc"
                        ? transactions.OrderBy(t => t.Id).ToList()
                        : transactions.OrderByDescending(t => t.Id).ToList(),
                    "type" => orderDir.ToLower() == "asc"
                        ? transactions.OrderBy(t => t.Type).ToList()
                        : transactions.OrderByDescending(t => t.Type).ToList(),
                    "quantity" => orderDir.ToLower() == "asc"
                        ? transactions.OrderBy(t => t.Quantity).ToList()
                        : transactions.OrderByDescending(t => t.Quantity).ToList(),
                    "price" => orderDir.ToLower() == "asc"
                        ? transactions.OrderBy(t => t.Price).ToList()
                        : transactions.OrderByDescending(t => t.Price).ToList(),
                    "amount" => orderDir.ToLower() == "asc"
                        ? transactions.OrderBy(t => t.Amount).ToList()
                        : transactions.OrderByDescending(t => t.Amount).ToList(),
                    "date" => orderDir.ToLower() == "asc"
                        ? transactions.OrderBy(t => t.Date).ToList()
                        : transactions.OrderByDescending(t => t.Date).ToList(),
                    "fees" => orderDir.ToLower() == "asc"
                        ? transactions.OrderBy(t => t.Fees).ToList()
                        : transactions.OrderByDescending(t => t.Fees).ToList(),
                    _ => transactions.OrderByDescending(t => t.Date).ToList()
                };
            }

            // Apply paging
            transactions = transactions
                .Skip(start)
                .Take(length)
                .ToList();

            // DataTables response format
            var response = new
            {
                draw = draw,
                recordsTotal = totalRecords,
                recordsFiltered = filteredRecords,
                data = transactions
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransaction(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null)
                return NotFound();
            return Ok(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionDto transactionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdTransaction = await _transactionService.CreateTransactionAsync(transactionDto);
            return CreatedAtAction(nameof(GetTransaction), new { id = createdTransaction.Id }, createdTransaction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, [FromBody] TransactionDto transactionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedTransaction = await _transactionService.UpdateTransactionAsync(id, transactionDto);
            if (updatedTransaction == null)
                return NotFound();

            return Ok(updatedTransaction);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var deleted = await _transactionService.DeleteTransactionAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}