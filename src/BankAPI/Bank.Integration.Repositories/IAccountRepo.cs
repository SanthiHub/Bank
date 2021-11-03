namespace Bank.Integration.Repositories
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Bank.Business.Models;

  /// <summary>
  ///  Operations related account repo
  public interface IAccountRepo
  {
    /// <summary>
    /// Retrieves all accounts
    /// </summary>
    /// <returns>List of all accounts</returns>
    Task<IEnumerable<Account>> GetAccountsAsync();

    /// <summary>
    ///Retrieves  account information
    /// </summary>
    /// <param name="id">Acccount id</param>
    /// <returns>Account dtails</returns>
    Task<Account> GetAccountAsync(Guid id);


    /// <summary>
    /// Creates an account
    /// </summary>
    /// <param name="createAccount">Account details</param>
    /// <returns>An awaitable task</returns>/
    Task CreateAccountAsync(CreateAccount createAccount);

    /// <summary>
    /// Updates account
    /// </summary>
    /// <param name="account">Account Deails</param>
    /// <returns>Updates account</returns>
    Task UpdateAccountAsync(Account account);
  }
}