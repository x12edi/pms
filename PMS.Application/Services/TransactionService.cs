using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMS.Application.DTOs;
using PMS.Application.Interfaces;
using PMS.Domain.Entities;
using PMS.Domain.Interfaces;
using System.Collections.Generic;
using System.Net.Http.Headers;
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

        //public async Task<TransactionDto> GetTransactionByIdAsync(int id)
        //{
        //    var transaction = await _unitOfWork.Transactions.GetByIdAsync(id);
        //    if (transaction == null)
        //        return null;
        //    var holding = await _unitOfWork.Holdings.GetByIdAsync(transaction.HoldingId);
        //    var portfolio = await _unitOfWork.Portfolios.GetByIdAsync(holding.PortfolioId);
        //    var asset = await _unitOfWork.Assets.GetByIdAsync(holding.AssetId);
        //    //return _mapper.Map<TransactionDto>(transaction);
        //    return new TransactionDto
        //    {
        //        Id = transaction.Id,
        //        HoldingId = transaction.HoldingId,
        //        Type = transaction.Type,
        //        Quantity = transaction.Quantity,
        //        Price = transaction.Price,
        //        Amount = transaction.Amount,
        //        Date = transaction.Date,
        //        Fees = transaction.Fees,
        //        PortfolioName = portfolio?.Name,
        //        HoldingTicker = asset?.Ticker
        //    };
        //}

        public async Task<TransactionDto> GetTransactionByIdAsync(int id)
        {
            var transaction = await (
                from t in _unitOfWork.Transactions.GetAll()
                join h in _unitOfWork.Holdings.GetAll() on t.HoldingId equals h.Id into holdingGroup
                from h in holdingGroup.DefaultIfEmpty()
                join p in _unitOfWork.Portfolios.GetAll() on h.PortfolioId equals p.Id into portfolioGroup
                from p in portfolioGroup.DefaultIfEmpty()
                join a in _unitOfWork.Assets.GetAll() on h.AssetId equals a.Id into assetGroup
                from a in assetGroup.DefaultIfEmpty()
                where t.Id == id
                select new
                {
                    Transaction = t,
                    Holding = h,
                    Portfolio = p,
                    Asset = a
                }
            ).FirstOrDefaultAsync();

            if (transaction == null || transaction.Transaction == null)
                return null;

            return new TransactionDto
            {
                Id = transaction.Transaction.Id,
                HoldingId = transaction.Transaction.HoldingId,
                Type = transaction.Transaction.Type,
                Quantity = transaction.Transaction.Quantity,
                Price = transaction.Transaction.Price,
                Amount = transaction.Transaction.Amount,
                Date = transaction.Transaction.Date,
                Fees = transaction.Transaction.Fees,
                PortfolioName = transaction.Portfolio?.Name,
                HoldingTicker = transaction.Asset?.Ticker
            };
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