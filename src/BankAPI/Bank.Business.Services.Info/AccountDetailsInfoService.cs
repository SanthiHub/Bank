namespace Bank.Business.Services.Info
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Bank.Business.Models;
  using Bank.Integration.Repositories;
  using Bank.Shared;

  /// <inheritdoc/>
  public class AccountDetailsInfoService : IAccountDetailsInfoService
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
    public AccountDetailsInfoService(IAccountRepo accountRepository, ICustomerRepo customerRepository)
    {
      this.accountRepository = accountRepository;
      this.customerRepository = customerRepository;
    }

    /// <inheritdoc/>
    public async Task<ServiceOutput<AccountInfo>> ExecuteAsync(Guid accountId)
    {

      var accountInfo = await this.accountRepository.GetAccountAsync(accountId);
      if (accountInfo != null)
      {
        var customer = await this.customerRepository.GetCustomerAsync(accountInfo.CustomerId);
        return new ServiceOutput<AccountInfo>(new AccountInfo()
        {
          Id = accountInfo.Id,
          Name = accountInfo.Name,
          Balance = accountInfo.Balance,
          Customer = customer
        });
      }

      return new ServiceOutput<AccountInfo>(
       null,
       new List<Message>() { (new Message("4", "Account does't exist", MessageSeverity.Error)) });
    }

  }
}