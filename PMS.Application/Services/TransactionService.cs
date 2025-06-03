using AutoMapper;
using PMS.Application.DTOs;
using PMS.Application.Interfaces;
using PMS.Domain.Entities;
using PMS.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactionsByHoldingAsync(int holdingId)
        {
            var transactions = await _unitOfWork.Transactions.FindAsync(h => h.HoldingId == holdingId);
            return _mapper.Map<IEnumerable<TransactionDto>>(transactions);
        }

        public async Task<TransactionDto> GetTransactionByIdAsync(int id)
        {
            var transaction = await _unitOfWork.Transactions.GetByIdAsync(id);
            return _mapper.Map<TransactionDto>(transaction);
        }

        public async Task<TransactionDto> CreateTransactionAsync(TransactionDto transactionDto)
        {
            var transaction = _mapper.Map<Transaction>(transactionDto);
            transaction.Date = System.DateTime.UtcNow;
            await _unitOfWork.Transactions.AddAsync(transaction);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<TransactionDto>(transaction);
        }

        public async Task<TransactionDto> UpdateTransactionAsync(int id, TransactionDto transactionDto)
        {
            var existingTransaction = await _unitOfWork.Transactions.GetByIdAsync(id);
            if (existingTransaction == null)
                return null;

            _mapper.Map(transactionDto, existingTransaction);
            existingTransaction.Date = System.DateTime.UtcNow; // Update date
            await _unitOfWork.Transactions.UpdateAsync(existingTransaction);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<TransactionDto>(existingTransaction);
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            var transaction = await _unitOfWork.Transactions.GetByIdAsync(id);
            if (transaction == null)
                return false;

            await _unitOfWork.Transactions.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}