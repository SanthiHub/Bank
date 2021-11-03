namespace Bank.Integration.Repositories
{
  using Bank.Business.Models;
  using Microsoft.EntityFrameworkCore;
  public sealed class BankAppDbContext : DbContext
  {

    public BankAppDbContext(DbContextOptions<BankAppDbContext> appDataContextOptions) : base(appDataContextOptions)
    {
      // no-op
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<TransferHistory> TransferHistories { get; set; }
  }
}
