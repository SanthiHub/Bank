namespace Bank.Business.Services
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Bank.Business.Model;
  using Bank.Business.Models;
  using Bank.Integration.Repositories;
  using Bank.Shared;

  /// <inheritdoc/>
  public class AddAccountService : IAddAccountService
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
    public AddAccountService(
    IAccountRepo accountRepository,
    ICustomerRepo customerRepository)
    {
      this.accountRepository = accountRepository;
      this.customerRepository = customerRepository;
    }

    /// <inheritdoc/>
    public async Task<ServiceOutput<Null>> ExecuteAsync(CustomerAccount customerAccount)
    {
      //  Step 1: Check if customer exists
      if ((await this.customerRepository.GetCustomersAsync()).Any(c => c.Name.Equals(customerAccount.CustomerName)))
      {
        return new ServiceOutput<Null>(
       null,
       new List<Message>() {
         new Message("1", "Customer Already exists. Please select another customer or use link account API", MessageSeverity.Error) });

      }
      // STEP 2: Create Customer
      var customerId = await this.customerRepository.CreateCustomerAsync(new CreateCustomer() { Name = customerAccount.CustomerName });

      // STEP 3: Create account
      await this.accountRepository
           .CreateAccountAsync(new CreateAccount() { Name = customerAccount.Name, Balance = customerAccount.Balance, CustomerId = customerId });
      return new ServiceOutput<Null>(null);
    }
  }
}
