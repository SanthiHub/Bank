namespace Bank.Business.Services
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Bank.Business.Models;
  using Bank.Integration.Repositories;
  using Bank.Shared;

  /// <inheritdoc/>
  public class AddAccountToCustomerService : IAddAccountToCustomerService
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
    public AddAccountToCustomerService(
    IAccountRepo accountRepository,
    ICustomerRepo customerRepository)
    {
      this.accountRepository = accountRepository;
      this.customerRepository = customerRepository;
    }

    /// <inheritdoc/>
    public async Task<ServiceOutput<Null>> ExecuteAsync(Account account)
    {
      // STEP 1: verify whether customer exists or not
      if (await this.customerRepository.GetCustomerAsync(account.CustomerId) == null)
      {
        return new ServiceOutput<Null>(
        null,
         new List<Message>() { new Message("2", "Customer doesn't Exist", MessageSeverity.Error) });
      }

      // STEP 2: Create account
      await this.accountRepository
             .CreateAccountAsync(new CreateAccount() { Name = account.Name, Balance = account.Balance, CustomerId = account.CustomerId });
      return new ServiceOutput<Null>(null);
    }
  }
}