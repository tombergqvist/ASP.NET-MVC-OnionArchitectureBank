using System;
using Xunit;
using Application.Commands.Transactions.Withdraw;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using System.Linq;
using Persistence;
using Application.Commands.Transactions.Transfer;
using Application.Commands.Transactions.Deposit;
using Application.Commands.Transactions.Interest;

namespace Test
{
    public class UnitTest
    {
        private DbContextOptions<BankDbContext> options;

        public UnitTest()
        {
            options = new DbContextOptionsBuilder<BankDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;
        }

        [Fact]
        public void WithdrawTooMuch()
        {
            using (var context = new BankDbContext(options))
            {
                int id = 1;
                var model = new WithdrawModel()
                {
                    AccountId = id,
                    Amount = "200.00"
                };
                var account = new Account()
                {
                    AccountId = id,
                    Balance = 100
                };

                context.Accounts.Add(account);
                context.SaveChanges();

                new WithdrawCommand().RunAsync(context, model).Wait();
                int numOfTransactions = context.Transactions.Where(t => t.AccountId == account.AccountId).Count();

                var createdAccount = context.Accounts.Single(a => a.AccountId == id);

                // Check balance
                Assert.Equal(100, createdAccount.Balance);

                // Checks so that no transaction was made
                Assert.Equal(0, numOfTransactions);
            }
        }

        [Fact]
        public void TransferTooMuch()
        {
            using (var context = new BankDbContext(options))
            {
                int fid = 2;
                int tid = 3;
                var model = new TransferModel()
                {
                    AccountId = fid,
                    ToAccount = tid,
                    Amount = "200.00"
                };
                var from = new Account()
                {
                    AccountId = fid,
                    Balance = 100
                };
                var to = new Account()
                {
                    AccountId = tid,
                    Balance = 0
                };
                context.Accounts.Add(from);
                context.Accounts.Add(to);
                context.SaveChanges();

                new TransferCommand().RunAsync(context, model).Wait();

                int numOfTransactions = context.Transactions
                    .Where(t => t.AccountId == from.AccountId || t.AccountId == to.AccountId)
                    .Count();

                var createdAccount1 = context.Accounts.Single(a => a.AccountId == fid);
                var createdAccount2 = context.Accounts.Single(a => a.AccountId == tid);

                // Check balance
                Assert.Equal(100, createdAccount1.Balance);
                Assert.Equal(0, createdAccount2.Balance);

                // Checks so that no transaction was made
                Assert.Equal(0, numOfTransactions);
            }
        }

        [Fact]
        public void DepositNegative()
        {
            using (var context = new BankDbContext(options))
            {
                int id = 4;
                var model = new DepositModel()
                {
                    AccountId = id,
                    Amount = "-200.00",
                };
                var account = new Account()
                {
                    AccountId = id,
                    Balance = 100
                };
                context.Accounts.Add(account);
                context.SaveChanges();

                new DepositCommand().RunAsync(context, model).Wait();

                int numOfTransactions = context.Transactions
                    .Where(t => t.AccountId == account.AccountId)
                    .Count();

                var createdAccount1 = context.Accounts.Single(a => a.AccountId == id);

                // Check balance
                Assert.Equal(100, createdAccount1.Balance);

                // Checks so that no transaction was made
                Assert.Equal(0, numOfTransactions);
            }
        }

        [Fact]
        public void WithdrawNegative()
        {
            using (var context = new BankDbContext(options))
            {
                int id = 5;
                var model = new WithdrawModel()
                {
                    AccountId = id,
                    Amount = "-50.00",
                };
                var account = new Account()
                {
                    AccountId = id,
                    Balance = 100
                };
                context.Accounts.Add(account);
                context.SaveChanges();

                new WithdrawCommand().RunAsync(context, model).Wait();

                int numOfTransactions = context.Transactions
                    .Where(t => t.AccountId == account.AccountId)
                    .Count();

                var createdAccount1 = context.Accounts.Single(a => a.AccountId == id);

                // Check balance
                Assert.Equal(100, createdAccount1.Balance);

                // Checks so that no transaction was made
                Assert.Equal(0, numOfTransactions);
            }
        }

        [Fact]
        public void TransactionsMade()
        {
            using (var context = new BankDbContext(options))
            {
                int fid = 6;
                int tid = 7;
                var model = new TransferModel()
                {
                    AccountId = fid,
                    ToAccount = tid,
                    Amount = "50.00"
                };
                var from = new Account()
                {
                    AccountId = fid,
                    Balance = 50
                };
                var to = new Account()
                {
                    AccountId = tid,
                    Balance = 0
                };
                context.Accounts.Add(from);
                context.Accounts.Add(to);
                context.SaveChanges();

                new TransferCommand().RunAsync(context, model).Wait();

                int numOfTransactions = context.Transactions
                    .Where(t => t.AccountId == from.AccountId || t.AccountId == to.AccountId)
                    .Count();

                var createdAccount1 = context.Accounts.Single(a => a.AccountId == fid);
                var createdAccount2 = context.Accounts.Single(a => a.AccountId == tid);

                // Check balance
                Assert.Equal(0, createdAccount1.Balance);
                Assert.Equal(50, createdAccount2.Balance);

                // Checks so that no transaction was made
                Assert.Equal(2, numOfTransactions);
            }
        }

        [Fact]
        public void Interest()
        {
            using (var context = new BankDbContext(options))
            {
                int id = 8;
                decimal interest = 0.135m;
                DateTime now = DateTime.Now;
                DateTime latestInterest = now.AddDays(-365);
                decimal balance = 100m;
                decimal expected = 113.5m;

                var model = new InterestModel()
                {
                    AccountId = id
                };
                var account = new Account()
                {
                    AccountId = id,
                    Balance = balance
                };
                context.Accounts.Add(account);
                context.SaveChanges();

                new InterestCommand().RunAsync(context, model, latestInterest, interest).Wait();

                int numOfTransactions = context.Transactions
                    .Where(t => t.AccountId == account.AccountId || t.AccountId == account.AccountId)
                    .Count();

                var createdAccount1 = context.Accounts.Single(a => a.AccountId == id);

                // Check balance
                Assert.Equal(expected, createdAccount1.Balance);

                // Checks so that a transaction was made
                Assert.Equal(1, numOfTransactions);
            }
        }
    }
}
