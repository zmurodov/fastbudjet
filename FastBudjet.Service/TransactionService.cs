using FastBudjet.Data;
using FastBudjet.Data.Models;
using FastBudjet.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastBudjet.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction> Add(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public Task Delete(int transactionId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return _context.Transactions
                .Include(x => x.AccountHistories)
                .Include(x => x.Category)
                .OrderByDescending(x => x.CreatedTime)
                .ToList();
        }

        public Task<Transaction> GetById(int transactionId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetTransactionsByCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetTransactionsByDate(DateTime startDate)
        {
            return _context.Transactions
                .Include(t => t.Category)
                .Where(t => t.CreatedTime <= startDate);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByParams(DateTime? startDate, DateTime? endDate, bool? income, int? accountId)
        {
            var q = _context.Transactions
                .Include(x => x.AccountHistories)
                .Include(x => x.Account)
                .Include(x => x.Category)
                .OrderByDescending(x => x.CreatedTime)
                .AsQueryable();

            if (accountId != null && accountId != 0)
            {
                q = q.Where(x => x.AccountId == accountId);
            }

            if (income != null)
            {
                q = q.Where(x => x.Income == income);
            }

            if (startDate != null)
            {
                q = q.Where(x => x.CreatedTime >= startDate);
            }
            if(endDate != null)
            {
                q = q.Where(x => x.CreatedTime <= endDate);
            }

            var result = await q.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByPeriod(DateTime startDate, DateTime endDate)
        {
            return await _context.Transactions
                .Include(x => x.AccountHistories)
                .Include(x => x.Account)
                .Include(t => t.Category)
                .Where(t => t.CreatedTime >= startDate && t.CreatedTime <= endDate)
                .ToListAsync();
        }

        public IEnumerable<Transaction> GetTransactionsByPeriodCategory(DateTime startDate, DateTime endDate, bool income)
        {
            return _context.Transactions
                //.Include(t => t.Category)
                .Where(t => t.CreatedTime <= startDate && t.CreatedTime <= endDate && t.Category.Income == income);
        }

        public IEnumerable<Transaction> GetTransactionsByPeriodCategoryWithAccount(DateTime startDate, DateTime endDate, bool income, int accountId)
        {
            return _context.Transactions
                .Where(t => t.CreatedTime <= startDate && t.CreatedTime <= endDate && t.Category.Income == income && t.AccountId == accountId);

        }

        public async Task<IEnumerable<Transaction>> GetTransactionsMonthly(DateTime startDate, DateTime endDate, bool income)
        {
            return await _context.Transactions
                .Include(x => x.AccountHistories)
                .Include(x => x.Account)
                .Include(x => x.Category)
                .Where(x => x.Income == income && x.CreatedTime >= startDate && x.CreatedTime <= endDate)
                .OrderByDescending(x => x.CreatedTime)
                .ToListAsync();
        }

        public Task<Transaction> Update(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
