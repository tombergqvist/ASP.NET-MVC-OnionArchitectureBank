using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    public class MockDbContext : DbContext, IBankDbContext
    {
        public MockDbContext(DbContextOptions<MockDbContext> options)
        : base(options)
        { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Disposition> Dispositions { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<PermanentOrder> PermanentOrder { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
