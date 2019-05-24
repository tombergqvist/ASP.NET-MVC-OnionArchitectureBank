using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IBankDbContext
    {
        DbSet<Accounts> Accounts { get; set; }
        DbSet<Cards> Cards { get; set; }
        DbSet<Customers> Customers { get; set; }
        DbSet<Dispositions> Dispositions { get; set; }
        DbSet<Loans> Loans { get; set; }
        DbSet<PermanentOrder> PermanentOrder { get; set; }
        DbSet<Transactions> Transactions { get; set; }

        //Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
