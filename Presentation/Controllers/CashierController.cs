using Application.Interfaces;
using Application.Queries.Accounts.AccountDetails;
using Application.Queries.Accounts.CustomerAccounts;
using Application.Queries.Customers.CustomerDetails;
using Application.Queries.Customers.CustomerSearch;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Cashier;

namespace Presentation.Controllers
{
    public class CashierController : Controller
    {
        IBankDbContext _context;

        public CashierController(IBankDbContext context)
        {
            _context = context;
        }

        public IActionResult EditCustomers()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Account(int id)
        {
            return View(new AccountViewModel()
            {
                Account = new AccountDetailsQuery().Get(_context, id)
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