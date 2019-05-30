using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Queries.Accounts.CustomerAccounts;
using Application.Queries.Customers.CustomerDetails;
using Application.Queries.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Presentation.Models.API;

namespace Presentation.Controllers
{
    [Route("api/")]
    [ApiController]
    public class APIController : ControllerBase
    {

        private readonly IBankDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public APIController(IBankDbContext context, IConfiguration config, 
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _configuration = config;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (result.Succeeded)
            {
                var signingKey = Convert.FromBase64String(_configuration["JWT:SigningSecret"]);
                var expiryDuration = int.Parse(_configuration["JWT:Expiry"]);

                var user = await _signInManager.UserManager.FindByNameAsync(model.Username);
                //var userPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                //var identity = userPrincipal.Identity;
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = null,              
                    Audience = null,            
                    IssuedAt = DateTime.UtcNow,
                    NotBefore = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddMinutes(expiryDuration),
                    Subject = new ClaimsIdentity(new List<Claim> {
                        new Claim("userid", user.Id)
                    }),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);

                var token = jwtTokenHandler.WriteToken(jwtToken);
                return Ok(token);
            }
            return Unauthorized();
        }

        [HttpGet]
        [Route("me")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<JsonResult> Customer()
        {
            var userId = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "userid").Value;
            var user = await _userManager.FindByIdAsync(userId);
            if(user.CustomerId != null)
            {
                var customerId = (int)user.CustomerId;

                var DTO = new CustomerDTO
                {
                    Customer = new CustomerDetailsQuery().Get(_context, customerId),
                    Accounts = new CustomerAccountsQuery().Get(_context, customerId)
                };

                return new JsonResult(DTO);
            }
            return new JsonResult("Error 1: The specified user account has no customer attached. ");
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