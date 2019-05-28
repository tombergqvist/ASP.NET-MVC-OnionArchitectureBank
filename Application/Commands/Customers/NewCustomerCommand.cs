using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Customers
{
    public class NewCustomerCommand
    {
        public async Task RunAsync(IBankDbContext context, NewCustomerCommandModel model)
        {
            var foundCustomer = context.Customers.SingleOrDefault(c => c.CustomerId == model.CustomerId);

            if (foundCustomer == null)
            {
                var customer = NewCustomer(model);
                await context.Customers.AddAsync(customer);
                await context.SaveChangesAsync(new System.Threading.CancellationToken());

                Account account = new Account()
                {
                    Balance = 0,
                    Created = DateTime.Now,
                    Frequency = "Monthly"
                };
                await context.Accounts.AddAsync(account);
                await context.SaveChangesAsync(new System.Threading.CancellationToken());

                Disposition disposition = new Disposition()
                {
                    Account = account,
                    Customer = customer,
                    Type = "Owner"
                };
                await context.Dispositions.AddAsync(disposition);
            }
            else
            {
                EditCustomer(foundCustomer, model);
            }
            await context.SaveChangesAsync(new System.Threading.CancellationToken());
        }

        // Helpers
        private void EditCustomer(Customer foundCustomer, NewCustomerCommandModel model)
        {
            foundCustomer.Birthday = model.Birthday;
            foundCustomer.City = model.City;
            foundCustomer.Country = model.Country;
            foundCustomer.CountryCode = model.CountryCode;
            foundCustomer.Emailaddress = model.Emailaddress;
            foundCustomer.Gender = model.Gender;
            foundCustomer.Givenname = model.Givenname;
            foundCustomer.Surname = model.Surname;
            foundCustomer.Streetaddress = model.Streetaddress;
            foundCustomer.NationalId = model.NationalId;
            foundCustomer.Telephonecountrycode = model.Telephonecountrycode;
            foundCustomer.Telephonenumber = model.Telephonenumber;
            foundCustomer.Zipcode = model.Zipcode;
        }

        private Customer NewCustomer(NewCustomerCommandModel model)
        {
            return new Customer()
            {
                Birthday = model.Birthday,
                City = model.City,
                Country = model.Country,
                CountryCode = model.CountryCode,
                Emailaddress = model.Emailaddress,
                Gender = model.Gender,
                Givenname = model.Givenname,
                Surname = model.Surname,
                Streetaddress = model.Streetaddress,
                NationalId = model.NationalId,
                Telephonecountrycode = model.Telephonecountrycode,
                Telephonenumber = model.Telephonenumber,
                Zipcode = model.Zipcode
            };
        }
    }
}