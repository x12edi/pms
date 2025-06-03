using PMS.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>> GetTransactionsByHoldingAsync(int holdingId);
        Task<TransactionDto> GetTransactionByIdAsync(int id);
        Task<TransactionDto> CreateTransactionAsync(TransactionDto transactionDto);
        Task<TransactionDto> UpdateTransactionAsync(int id, TransactionDto transactionDto);
        Task<bool> DeleteTransactionAsync(int id);
    }
}