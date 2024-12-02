using AutoMapper;
using FastBudjet.Data.Models;
using FastBudjet.Web.Models.TransactionViewModel;
using FastBudjet.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FastBudjet.Web.Models;
using FastBudjet.Web.Models.AccountsViewModel;
using FastBudjet.Web.Models.HomeViewModel;

namespace FastBudjet.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountsService _accountsService;
        private readonly ICategoryService _categoryService;
        private readonly IAccountHistoryService _accountHistoryService;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public HomeController(IAccountsService accountsService,
            ICategoryService categoryService,
            IAccountHistoryService accountHistoryService,
            ITransactionService transactionService,
            IMapper mapper)
        {
            _accountsService = accountsService;
            _categoryService = categoryService;
            _accountHistoryService = accountHistoryService;
            _transactionService = transactionService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var accounts = _accountsService.GetAccounts();
            var incomingCategories = _categoryService.GetIncomingCategories();
            var expenseCategories = _categoryService.GetExpenseCategories();

            decimal summary = 0;
            foreach (var a in accounts)
            {
                summary += a.Balance;
            }
            var summaryStr = BalanceToStr(summary);

            var now = DateTime.Now;
            var thisMonth = GetThisMonthDate(now);
            var lastMonth = GetLastMonthDate(now);

            var thisMonthStat = await GetMonthlyStatViewModel(thisMonth);
            var lastMonthStat = await GetMonthlyStatViewModel(lastMonth);

            var accountsDto = _mapper.Map<IEnumerable<Account>, IEnumerable<AccountViewModel>>(accounts);

            var dailyStats = await GetMonthDailyStat(thisMonth);

            return View(new Models.HomeViewModel.IndexViewModel
            {
                Accounts = accountsDto,
                IncomingCategories = incomingCategories,
                ExpenseCategories = expenseCategories,
                ThisMonthStat = thisMonthStat,
                LastMonthStat = lastMonthStat,
                Summary = summaryStr,
                MonthDailyStats = dailyStats,
                CreateViewModel = new Models.TransactionViewModel.CreateViewModel(),
            });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            var accounts = _accountsService.GetAccounts();
            var result = new List<AccountHistory>();

            var now = DateTime.Now;
            var dateFrom = new DateTime(now.Year, now.Month, 1);

            foreach (var account in accounts)
            {
                var ah = await _accountHistoryService.GetAccountHistoriesByDateAccountIncome(account, dateFrom, true);
                result.Add(ah);
            }

            return Ok(result);
        }

        private async Task<IEnumerable<Transaction>> GetTransactionsMonthSummaryWithType(DateTime dateTime, bool income)
        {
            var daysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
            var dateTo = dateTime.AddDays(daysInMonth);
            return await _transactionService.GetTransactionsMonthly(dateTime, dateTo, income);
        }

        private decimal CalculateTransactionsSummary(IEnumerable<Transaction> transactions)
        {
            decimal res = 0;
            foreach (var t in transactions)
            {
                if (t.Income)
                    res += t.Amount;
                else
                    res -= t.Amount;

            }
            return res;
        }

        private string BalanceToStr(decimal balance)
        {
            var res = balance.ToString("c", CultureInfo.CreateSpecificCulture("uz-Latn-UZ"));
            return res.Substring(0, res.IndexOf(" soʻm"));
        }

        private DateTime GetThisMonthDate(DateTime now)
        {
            return new DateTime(now.Year, now.Month, 1);
        }

        private DateTime GetLastMonthDate(DateTime now)
        {
            var month = now.Month - 1;
            var year = now.Year;
            if (month == 0)
            {
                month = 12;
                year -= 1;
            }
            return new DateTime(year, month, 1);
        }

        private async Task<MonthStatViewModel> GetMonthlyStatViewModel(DateTime date)
        {
            var monthIncomes = await GetTransactionsMonthSummaryWithType(date, true);
            var monthExpenses = await GetTransactionsMonthSummaryWithType(date, false);

            var mIncomeBalance = CalculateTransactionsSummary(monthIncomes);
            var mExpenseBalance = Math.Abs(CalculateTransactionsSummary(monthExpenses));
            var mDiffBalance = mIncomeBalance - mExpenseBalance;

            var mIncomeBalanceStr = BalanceToStr(mIncomeBalance);
            var mExpenseBalanceStr = BalanceToStr(mExpenseBalance);
            var mDiffStr = BalanceToStr(mDiffBalance);

            return new MonthStatViewModel
            {
                Expense = mExpenseBalanceStr,
                Income = mIncomeBalanceStr,
                Diff = mDiffStr,
                ExpenseNum = mExpenseBalance,
                IncomeNum = mIncomeBalance,
            }; ;
        }

        private async Task<List<DailyStatViewModel>> GetMonthDailyStat(DateTime date)
        {
            var res = new List<DailyStatViewModel>();
         
            var days = DateTime.DaysInMonth(date.Year, date.Month);

            for (int i = 0; i < days; i++)
            {
                var day = date.AddDays(i);
                var dateTo = day.AddHours(23).AddMinutes(59);

                var transactions = await _transactionService.GetTransactionsByPeriod(day, dateTo);
                var summary = CalculateTransactionsSummary(transactions);

                var summaryStr = BalanceToStr(summary);
                var dayToShort = day.ToString("d. MMM");

                var dailyStat = new DailyStatViewModel
                {
                    Summary = summary,
                    SummaryStr = summaryStr,
                    Day = dayToShort
                };

                res.Add(dailyStat);
            }

            return res;
        }
    }
}
