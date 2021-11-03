namespace Bank.Integration.Repositories
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Bank.Business.Models;
  using Microsoft.EntityFrameworkCore;

  /// <summary>
  ///     Membership repository
  /// </summary>

  public sealed class TransferHistoryRepo : ITransferHistoryRepo
  {
    /// <summary>
    /// Bank Data base context
    /// </summary>
    private readonly BankAppDbContext appDbContext;

    /// <summary>
    /// Designated constructor
    /// </summary>
    /// <param name="appDbContext">App database context</param>
    public TransferHistoryRepo(BankAppDbContext appDbContext)
    {
      this.appDbContext = appDbContext;
    }

    public async Task CreateTransferHistoryAsync(CreateTransferHistory createTransferHistory)
    {
      await this.appDbContext.AddAsync(new TransferHistory()
      {
        Amount = createTransferHistory.Amount,
        FromAccountId = createTransferHistory.FromAccountId,
        ToAccountId = createTransferHistory.ToAccountId,
        TransactionTime = DateTime.Now
      });
      await this.appDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<TransferHistory>> GetTransferHistoryAsync(Guid accountId)
    {
      return await this.appDbContext.TransferHistories
      .Where(transferHistory => transferHistory.FromAccountId.Equals(accountId) || transferHistory.ToAccountId.Equals(accountId)).ToListAsync();
    }

  }
}