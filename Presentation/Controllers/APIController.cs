using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Queries.Customers.CustomerDetails;
using Application.Queries.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/")]
    [ApiController]
    public class APIController : ControllerBase
    {

        IBankDbContext _context;

        public APIController(IBankDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("me")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        public JsonResult Customer()
        {
            var customer = new CustomerDetailsQuery().Get(_context, 1);
            return new JsonResult(customer);
        }

        [HttpGet]
        [Route("accounts/{id}/{limit}/{offset}")]
        public JsonResult Accounts(int id, int limit, int offset)
        {
            var transactions = new TransactionsQuery().Get(_context, id, limit, offset);
            return new JsonResult(transactions);
        }

    }

    
}