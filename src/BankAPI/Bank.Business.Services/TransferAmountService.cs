namespace Bank.Business.Services
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Bank.Business.Models;
  using Bank.Integration.Repositories;
  using Bank.Shared;

  /// <inheritdoc/>
  public class TransferAmountService : ITransferAmountService
  {
    /// <summary>
    /// Account repository
    /// </summary>
    private readonly IAccountRepo accountRepository;

    /// <summary>
    /// Transfer history repository
    /// </summary>
    private readonly ITransferHistoryRepo transferHistoryRepo;

    public TransferAmountService(
      IAccountRepo accountRepository,
      ITransferHistoryRepo transferHistoryRepo)
    {
      this.accountRepository = accountRepository;
      this.transferHistoryRepo = transferHistoryRepo;
    }

    /// <inheritdoc/>
    public async Task<ServiceOutput<Null>> ExecuteAsync(Transfer transfer)
    {
      // Step 1: Deduct amount
      var fromAccount = await this.accountRepository.GetAccountAsync(transfer.FromAccountId);
      var toAccount = await this.accountRepository.GetAccountAsync(transfer.ToAccountId);
      if (fromAccount == null || toAccount == null)
      {
        return new ServiceOutput<Null>(
        null,
         new List<Message>() { new Message("3", "Please verify accounts", MessageSeverity.Error) });
      }
      if (fromAccount.Balance < transfer.Amount)
      {
        return new ServiceOutput<Null>(
       null,
        new List<Message>() { new Message("4", "Account doesn't have sufficient funds", MessageSeverity.Error) });
      }
      fromAccount.Balance = fromAccount.Balance - transfer.Amount;
      await this.accountRepository.UpdateAccountAsync(fromAccount);

      // Step 2: Add amount
      toAccount.Balance = toAccount.Balance + transfer.Amount;
      await this.accountRepository.UpdateAccountAsync(toAccount);

      // Step 3: Save transfer details
      await this.transferHistoryRepo.CreateTransferHistoryAsync(
        new CreateTransferHistory()
        {
          Amount = transfer.Amount,
          FromAccountId = transfer.FromAccountId,
          ToAccountId = transfer.ToAccountId
        });

      return new ServiceOutput<Null>(null);
    }

  }
}