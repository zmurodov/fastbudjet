using AutoMapper;
using FastBudjet.Data.Models;
using FastBudjet.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FastBudjet.Web.Models.AccountsViewModel;

namespace FastBudjet.Web.Controllers
{
    [Route("[controller]")]
    public class AccountsController : Controller
    {
        private readonly IAccountsService _accountsService;
        private readonly IMapper _mapper;

        public AccountsController(IAccountsService accountsService, IMapper mapper)
        {
            _accountsService = accountsService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var accounts = _accountsService.GetAccounts();

            decimal summary = 0;
            foreach (var a in accounts)
            {
                summary += a.Balance;
            }
            var summaryStr = summary.ToString("c", CultureInfo.CreateSpecificCulture("uz-Latn-UZ"));
            summaryStr = summaryStr.Substring(0, summaryStr.IndexOf(" soʻm"));

            return View(new IndexViewModel
            {
                Accounts = accounts,
                CreateViewModel = new CreateViewModel(),
                UpdateViewModel = new UpdateViewModel(),
                Summary = summaryStr
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel createViewModel)
        {
            var account = _mapper.Map<CreateViewModel, Account>(createViewModel);

            await _accountsService.Add(account);

            return RedirectToAction("Index");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null) return NotFound();
            var account = await _accountsService.GetById(id.Value);

            return Ok(account);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(UpdateViewModel updateViewModel)
        {
            if (updateViewModel.Id == null)
            {
                return NotFound();
            }
            var account = await _accountsService.GetById(updateViewModel.Id.Value);

            account.Name = updateViewModel.Name ?? account.Name;
            account.Description = updateViewModel.Description;
            account.Balance = updateViewModel.Balance.Value;
            

            await _accountsService.Update(account);

            return RedirectToAction("Index");
        }

        [HttpPost("{id}/delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            await _accountsService.DeleteById(id.Value);

            return Ok();
        }
    }
}
