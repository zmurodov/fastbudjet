using FastBudjet.Data;
using FastBudjet.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FastBudjet.Core.Interfaces
{
    public class AccountHistoriesService : IAccountHistoryService
    {
        private readonly ApplicationDbContext _context;

        public AccountHistoriesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AccountHistory> Add(AccountHistory AccountHistory)
        {
            await _context.AccountHistories.AddAsync(AccountHistory);
            await _context.SaveChangesAsync();
            return AccountHistory;
        }

        public async Task DeleteById(int id)
        {
            var ac = await _context.AccountHistories.FirstOrDefaultAsync(x => x.Id == id);
            _context.Remove(ac);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<AccountHistory> GetAccountHistories()
        {
            throw new NotImplementedException();
        }

        public async Task<AccountHistory> GetAccountHistoriesByDateAccountIncome(Account account, DateTime dateFrom, bool Income)
        {
            var dateTo = dateFrom.AddDays(31).AddMinutes(1);

            return await _context.AccountHistories
                .Where(x => x.AccountId == account.Id && x.Transaction.Income == Income && x.Transaction.CreatedTime >= dateFrom && x.Transaction.CreatedTime <= dateTo)
                .OrderByDescending(x => x.Transaction.CreatedTime)
                .FirstOrDefaultAsync();
        }

        public async Task<AccountHistory> GetById(int id)
        {
            return await _context.AccountHistories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<AccountHistory> Update(AccountHistory AccountHistory)
        {
            _context.Update(AccountHistory);
            await _context.SaveChangesAsync();
            return AccountHistory;
        }
    }
}
