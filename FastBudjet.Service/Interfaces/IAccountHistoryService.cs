using FastBudjet.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FastBudjet.Service.Interfaces
{
    public interface IAccountHistoryService
    {
        Task<AccountHistory> GetById(int id);

        Task<AccountHistory> Add(AccountHistory AccountHistory);

        Task<AccountHistory> Update(AccountHistory AccountHistory);

        Task DeleteById(int id);

        IEnumerable<AccountHistory> GetAccountHistories();
        Task<AccountHistory> GetAccountHistoriesByDateAccountIncome(Account account, DateTime dateTime, bool Income);
    }
}
