using FastBudjet.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FastBudjet.Service.Interfaces
{
    public interface ITransactionService
    {
        Task<Transaction> GetById(int transactionId);

        Task<Transaction> Add(Transaction transaction);

        Task<Transaction> Update(Transaction transaction);

        Task Delete(int transactionId);

        IEnumerable<Transaction> GetAllTransactions();
        IEnumerable<Transaction> GetTransactionsByDate(DateTime startDate);

        Task<IEnumerable<Transaction>> GetTransactionsByPeriod(DateTime startDate, DateTime endDate);
        IEnumerable<Transaction> GetTransactionsByPeriodCategory(DateTime startDate, DateTime endDate, bool income);
        IEnumerable<Transaction> GetTransactionsByPeriodCategoryWithAccount(DateTime startDate, DateTime endDate, bool income, int accountId);
        Task<IEnumerable<Transaction>> GetTransactionsByParams(DateTime? startDate, DateTime? endDate, bool? income, int? accountId);
        Task<IEnumerable<Transaction>> GetTransactionsMonthly(DateTime startDate, DateTime endDate, bool income);

        IEnumerable<Transaction> GetTransactionsByCategory(Category category);

    }
}
