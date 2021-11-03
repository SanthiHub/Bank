namespace Bank.Business.Services.Info
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Bank.Business.Models;
  using Bank.Integration.Repositories;
  using Bank.Shared;

  /// <inheritdoc/>
  public class AccountsDetailsInfoService : IAccountsDetailsInfoService
  {
    /// <summary>
    /// Account repository
    /// </summary>/
    private readonly IAccountRepo accountRepository;

    /// <summary>
    /// Customer repository
    /// </summary>
    private readonly ICustomerRepo customerRepository;

    /// <summary>
    ///  Designated Constructor.
    /// </summary>
    /// <param name="accountRepository">Account repository</param>
    /// <param name="customerRepository">Customer repository</param>
    public AccountsDetailsInfoService(IAccountRepo accountRepository, ICustomerRepo customerRepository)
    {
      this.accountRepository = accountRepository;
      this.customerRepository = customerRepository;
    }

    /// <inheritdoc/>
    public async Task<ServiceOutput<IEnumerable<AccountInfo>>> ExecuteAsync(Null nullInput)
    {
      var accounts = (
      from account in await this.accountRepository.GetAccountsAsync()
      select new AccountInfo()
      {
        Id = account.Id,
        Name = account.Name,
        Balance = account.Balance,
        Customer = this.customerRepository.GetCustomerAsync(account.CustomerId).Result
      });

      return new ServiceOutput<IEnumerable<AccountInfo>>(accounts ?? new List<AccountInfo>());
    }
  }
}