namespace Bank.Integration.Repositories
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Bank.Business.Models;

  /// <summary>
  ///  Operations related transfer history Repo
  /// </summary>
  public interface ITransferHistoryRepo
  {

    /// <summary>
    /// Retrives transaction history for the selected account
    /// </summary>
    /// <param name="accountId">Account id</param>
    /// <returns>List of transfer details</returns>
    Task<IEnumerable<TransferHistory>> GetTransferHistoryAsync(Guid accountId);

    /// <summary>
    /// Create transfer history
    /// </summary>
    /// <param name="transferHistory">transfer details</param>
    /// <returns>An Awaitable task</returns>
    Task CreateTransferHistoryAsync(CreateTransferHistory transferHistory);

  }
}