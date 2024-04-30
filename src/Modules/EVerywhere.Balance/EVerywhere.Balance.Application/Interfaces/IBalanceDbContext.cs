using EVerywhere.Balance.Domain.Entities;
using EVerywhere.ModulesCommon.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace EVerywhere.Balance.Application.Interfaces;

public interface IBalanceDbContext : IDbContext
{
    DbSet<PaymentMethod> PaymentMethods { get; set; }
    
    DbSet<Payment> Payments { get; set; }
    
    DbSet<Hold> Holds { get; set; }
    
    DbSet<PaymentSystemConfiguration> PaymentSystemConfigurations { get; set; }
    
    DbSet<PaymentSystemWidget> PaymentSystemWidget { get; set; }
    
    DbSet<Receipt> Receipts { get; set; }
    
    DbSet<Debtor> Debtors { get; set; }
}