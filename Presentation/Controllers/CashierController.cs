using Application.Interfaces;
using Application.Commands.Customers;
using Application.Queries.Accounts.AccountDetails;
using Application.Queries.Accounts.CustomerAccounts;
using Application.Queries.Customers.CustomerDetails;
using Application.Queries.Customers.CustomerSearch;
using Application.Queries.Transactions;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Cashier;
using System.Threading.Tasks;
using Application.Commands.Transactions.Transfer;
using Application.Commands.Transactions.Withdraw;
using Application.Commands.Transactions.Deposit;
using Application.Queries.Interest.LatestInterestDate;
using System;
using Application.Commands.Transactions.Interest;

namespace Presentation.Controllers
{
    public class CashierController : Controller
    {
        private readonly IBankDbContext _context;

        public CashierController(IBankDbContext context)
        {
            _context = context;
        }

        public IActionResult ManageCustomers()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Transaction()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Deposit()
        {
            return View(new DepositViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deposit(int id, DepositViewModel model)
        {
            string msg = null;
            if (ModelState.IsValid)
            {
                msg = await new DepositQuery().RunAsync(_context, model.Deposit);
            }
            model.Message = msg;
            return View(model);
        }

        [HttpGet]
        public IActionResult Interest()
        {
            return View(new InterestViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Interest(InterestViewModel model)
        {
            string msg = null;
            if (ModelState.IsValid)
            {
                DateTime date = new LatestInterestDateQuery().Get(_context, model.Interest.AccountId);
                msg = await new InterestQuery().RunAsync(_context, model.Interest, date, 0.10m);
            }
            model.Message = msg;
            return View(model);
        }

        [HttpGet]
        public IActionResult Withdraw()
        {
            return View(new WithdrawViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Withdraw(int id, WithdrawViewModel model)
        {
            string msg = null;
            if (ModelState.IsValid)
            {
                msg = await new WithdrawQuery().RunAsync(_context, model.Withdraw);
            }
            model.Message = msg;
            return View(model);
        }

        [HttpGet]
        public IActionResult Transfer()
        {
            return View(new TransferViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Transfer(int id, TransferViewModel model)
        {
            string msg = null;
            if (ModelState.IsValid)
            {
                msg = await new TransferCommand().RunAsync(_context, model.Transfer);
            }
            model.Message = msg;
            return View(model);
        }

        [HttpGet]
        public IActionResult EditCustomer(int? id)
        {
            if(id == null)
                return View(new NewCustomerCommandModel());

            var query = new CustomerDetailsQuery().Get(_context, (int)id);
            return View(new NewCustomerCommandModel()
            {
                Birthday = query.Birthday,
                City = query.City,
                Country = query.Country,
                CountryCode = query.CountryCode,
                CustomerId = query.CustomerId,
                Emailaddress = query.Emailaddress,
                Gender = query.Gender,
                Givenname = query.Givenname,
                Surname = query.Surname,
                NationalId = query.NationalId,
                Streetaddress = query.Streetaddress,
                Telephonecountrycode = query.Telephonecountrycode,
                Telephonenumber = query.Telephonenumber,
                Zipcode = query.Zipcode
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCustomer(NewCustomerCommandModel model)
        {
            if (ModelState.IsValid)
            {
                await new NewCustomerCommand().RunAsync(_context, model);
                return RedirectToAction("Index", "Home");
            }
            return View(model);

        }

        [HttpGet]
        public IActionResult Account(int id)
        {
            return View(new AccountViewModel()
            {
                Account = new AccountDetailsQuery().Get(_context, id),
                TransactionsList = new TransactionsQuery().Get(_context, id, 0)
            });
        }

        [HttpGet]
        public IActionResult GetTransactions(int id, int page)
        {
            var query = new TransactionsQuery().Get(_context, id, page);
            return PartialView("_TransactionsPartial", new TransactionsViewModel()
            {
                Transactions = query.Transactions,
                Id = id,
                Page = page + 1,
                HasMore = query.HasMore
            });
        }

        [HttpGet]
        public IActionResult Customer(int? id)
        {
            if(id == null)
            {
                return View();
            }
            else
            {
                return View(new CustomerViewModel()
                {
                    Details = new CustomerDetailsQuery().Get(_context, (int)id),
                    AccountsModel = new CustomerAccountsQuery().Get(_context, (int)id),
                });
            }
        }

        [HttpGet]
        public IActionResult CustomerSearch()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetCustomers(string name, string city, int page)
        {
            var query = new CustomerSearchQuery().Get(_context, name, city, page);
            return PartialView("_CustomerSearchResultPartial", new CustomerSearchViewModel()
            {
                Customers = query.Customers,
                Page = page + 1,
                Name = name,
                City = city,
                HasMore = query.HasMore
            });
        }
    }
}