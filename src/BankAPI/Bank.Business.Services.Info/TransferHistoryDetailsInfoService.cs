namespace Bank.Business.Services.Info
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Bank.Business.Models;
  using Bank.Integration.Repositories;

  /// <inheritdoc/>
  public class TransferHistoryDetailsInfoService : ITransferHistoryDetailsInfoService
  {
    /// <summary>
    /// Transfer history repository
    /// </summary>
    private readonly ITransferHistoryRepo transferHistoryRepository;

    /// <summary>
    /// Designated Constructor.
    /// </summary>
    /// <param name="transferHistoryRepository">Transfer history repository</param>
    public TransferHistoryDetailsInfoService(ITransferHistoryRepo transferHistoryRepository)
    {
      this.transferHistoryRepository = transferHistoryRepository;
    }

    /// <inheritdoc/>
    public async Task<ServiceOutput<IEnumerable<TransferHistory>>> ExecuteAsync(Guid accountId)
    {
      var transactionHistories = await this.transferHistoryRepository.GetTransferHistoryAsync(accountId);

      return new ServiceOutput<IEnumerable<TransferHistory>>(
        transactionHistories ?? new List<TransferHistory>());
    }
  }
}