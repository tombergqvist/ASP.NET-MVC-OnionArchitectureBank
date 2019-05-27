using Application.Interfaces;
using Application.Queries.Accounts.CustomerAccounts;
using Application.Queries.Customers.CustomerDetails;
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

        public IActionResult EditUsers()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Customer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Customer(int id)
        {
            return View(new CustomerViewModel()
            {
                Details = new CustomerDetailsQuery().Get(_context, id),
                AccountsModel = new CustomerAccountsQuery().Get(_context, id),
            });
        }
    }
}