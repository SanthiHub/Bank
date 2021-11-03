namespace Bank.Integration.Repositories
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Bank.Business.Models;
  using Microsoft.EntityFrameworkCore;

  /// <summary>
  ///     Account repository
  /// </summary>

  public sealed class AccountRepo : IAccountRepo
  {
    /// <summary>
    /// Bank Data base context
    /// </summary>
    private readonly BankAppDbContext appDbContext;

    /// <summary>
    /// Designated constructor
    /// </summary>
    /// <param name="appDbContext">App database context</param>
    public AccountRepo(BankAppDbContext appDbContext)
    {
      this.appDbContext = appDbContext;
    }

    public async Task CreateAccountAsync(CreateAccount createAccount)
    {
      var account = new Account()
      {
        Balance = createAccount.Balance,
        CustomerId = createAccount.CustomerId,
        Name = createAccount.Name
      };
      await this.appDbContext.AddAsync(account);
      await this.appDbContext.SaveChangesAsync();
    }

    public async Task UpdateAccountAsync(Account account)
    {
      this.appDbContext.Accounts.Update(account);
      await this.appDbContext.SaveChangesAsync();
    }

    public async Task<Account> GetAccountAsync(Guid id)
    {
      return await this.appDbContext.Accounts.FirstOrDefaultAsync(account => account.Id.Equals(id));
    }

    public async Task<IEnumerable<Account>> GetAccountsAsync()
    {
      return await this.appDbContext.Accounts.ToListAsync();
    }
  }
}