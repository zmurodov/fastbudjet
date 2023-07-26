using AutoMapper;
using FastBudjet.Data.Models;
using FastBudjet.Models.TransactionViewModel;
using FastBudjet.Service;
using FastBudjet.Service.Interfaces;
using FastBudjet.TransactionViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace FastBudjet.Controllers
{
    [Route("[controller]")]
    public class OperationsController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly ICategoryService _categoryService;
        private readonly IAccountsService _accountsService;
        private readonly IAccountHistoryService _accountHistoryService;
        private readonly IMapper _mapper;

        public OperationsController(ITransactionService transactionService,
            ICategoryService categoryService,
            IAccountsService accountsService,
            IAccountHistoryService accountHistoryService,
            IMapper mapper)
        {
            _transactionService = transactionService;
            _categoryService = categoryService;
            _accountsService = accountsService;
            _accountHistoryService = accountHistoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {

            var incomingCategories = _categoryService.GetIncomingCategories();
            var expenseCategories = _categoryService.GetExpenseCategories();
            var accounts = _accountsService.GetAccounts();

            var transactions = _transactionService.GetAllTransactions();

            var transactionDto = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactViewModel>>(transactions);

            decimal summary = 0;

            foreach(var a in accounts)
            {
                summary += a.Balance;
            }

            var summaryStr = summary.ToString("c", CultureInfo.CreateSpecificCulture("uz-Latn-UZ"));

            summaryStr = summaryStr.Substring(0, summaryStr.IndexOf(" soʻm"));

            return View(new IndexViewModel
            {
                CreateViewModel = new CreateViewModel(),
                UpdateViewModel = new UpdateViewModel(),
                FilterViewModel = new FilterViewModel(),
                IncomingCategories = incomingCategories,
                ExpenseCategories = expenseCategories,
                Accounts = accounts,
                Transactions = transactionDto,
                Summary = summaryStr
            });
        }

        [HttpPost("filter")]
        public async Task<ActionResult<IEnumerable<Transaction>>> Filter([FromBody] FilterViewModel filterViewModel)
        {
            
            var dateFrom = DateTime.Parse(filterViewModel.StartDate, CultureInfo.GetCultureInfo("ru-RU"));
            var dateTo = DateTime.Parse(filterViewModel.StartDate, CultureInfo.GetCultureInfo("ru-RU")).AddDays(1);
            var accountId = filterViewModel.AccountId;

            var accounts = _accountsService.GetAccounts();
            decimal summary = 0;
            foreach (var a in accounts)
            {
                summary += a.Balance;
            }
            var summaryStr = summary.ToString("c", CultureInfo.CreateSpecificCulture("uz-Latn-UZ"));
            summaryStr = summaryStr.Substring(0, summaryStr.IndexOf(" soʻm"));


            bool? income = null;

            if (filterViewModel.TransactionType == "1")
                income = true;
            else if (filterViewModel.TransactionType == "0")
                income = false;


            IEnumerable<Transaction> transactions = await _transactionService
                .GetTransactionsByParams(dateFrom, dateTo, income, accountId);

            var result = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactViewModel>>(transactions);
            return Ok(new ResultViewModel
            {
                Transactions = result,
                Summary = summaryStr
            });

        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel createViewModel)
        {
            var account = await _accountsService.GetById(createViewModel.AccountId);

            var transaction = new Transaction
            {
                Amount = createViewModel.Amount,
                CategoryId = createViewModel.CategoryId,
                CreatedTime = DateTime.Parse(createViewModel.CreatedTime, CultureInfo.GetCultureInfo("ru-RU")),
                Description = createViewModel.Description,
                Income = createViewModel.Income,
                SendedOn = DateTime.Parse(createViewModel.CreatedTime, CultureInfo.GetCultureInfo("ru-RU")),
                AccountId = createViewModel.AccountId
            };

            await _transactionService.Add(transaction);

            if (transaction.Income)
                account.Balance += transaction.Amount;
            else
                account.Balance -= Math.Abs(transaction.Amount);

            await _accountsService.Update(account);

            var accountHistory = new AccountHistory
            {
                AccountId = account.Id,
                Summary = account.Balance,
                TransactionId = transaction.Id
            };

            await _accountHistoryService.Add(accountHistory);

            return RedirectToAction("Index");
        }
    }
}
