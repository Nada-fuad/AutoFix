using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace AutoFix.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Customer> Customers { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);

    }
}
