namespace Bank.Business.Services.Info
{
  using System;
  using System.Collections.Generic;
  using Bank.Business.Models;
  using Bank.Shared;

  /// <summary>
  /// Retrieves transaction history for the requested account id.
  /// </summary>
  public interface ITransferHistoryDetailsInfoService : IAsyncService<Guid, ServiceOutput<IEnumerable<TransferHistory>>>
  {
    // no-op
  }
}