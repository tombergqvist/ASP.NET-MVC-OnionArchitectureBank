using Application.Commands.Customers;
using Application.Interfaces;
using Application.Queries.Accounts.AccountDetails;
using Application.Queries.Accounts.CustomerAccounts;
using Application.Queries.Customers.CustomerDetails;
using Application.Queries.Customers.CustomerSearch;
using Application.Queries.Transactions;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Cashier;
using System.Threading.Tasks;

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
        public IActionResult EditCustomer()
        {
            return View(new NewCustomerCommandModel());
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