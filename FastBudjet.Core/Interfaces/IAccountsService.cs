using FastBudjet.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FastBudjet.Core.Interfaces
{
    public interface IAccountsService
    {
        Task<Account> GetById(int id);

        Task<Account> Add(Account account);

        Task<Account> Update(Account account);

        Task DeleteById(int id);

        IEnumerable<Account> GetAccounts();
    }
}
