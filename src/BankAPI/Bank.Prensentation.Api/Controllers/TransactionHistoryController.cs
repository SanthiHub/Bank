namespace Bank.Presentation.Controllers
{
  using System.Collections.Generic;
  using Bank.Business.Models;
  using Bank.Presentation.Api.Contracts;
  using System;
  using Microsoft.AspNetCore.Mvc;
  using Bank.Business.Services.Info;
  using System.Threading.Tasks;
  using System.Linq;
  using Bank.Shared;

  /// <summary>
  ///  Provides services related to account transfer history.
  /// </summary>
  [Route("api/bank")]
  [ApiController]
  public sealed class TransactionHistoryController : ControllerBase
  {
    /// <summary>
    /// Transfer history details info service
    /// </summary>
    private readonly ITransferHistoryDetailsInfoService transferHistoryDetailsInfoService;

    /// <summary>
    /// Designated constructor
    /// </summary>
    /// <param name="transferHistoryDetailsInfoService">Transfer history details info service</param>
    public TransactionHistoryController(
    ITransferHistoryDetailsInfoService transferHistoryDetailsInfoService)
    {
      this.transferHistoryDetailsInfoService = transferHistoryDetailsInfoService;
    }

    /// <summary>
    /// Retrives transfer transaction history details for the given account
    /// </summary>
    /// <param name="accountId">Account id</param>
    /// <returns>List of transfer histories.</returns>
    [HttpGet("{accountId}/history")]
    public async Task<ApiResponse<IEnumerable<TransferHistoryDetails>>> GetAsync(Guid accountId)
    {
      var transferHistoryOutput = await this.transferHistoryDetailsInfoService.ExecuteAsync(accountId);

      this.HttpContext.Response.StatusCode =
       transferHistoryOutput.Messages.Any(m => m.Severity == MessageSeverity.Error) ? 404 : 200;

      return new ApiResponse<IEnumerable<TransferHistoryDetails>>()
      {
        Payload = this.toTransferHistoryDetails(accountId, transferHistoryOutput.Data),
        Messages = transferHistoryOutput.Messages
      };
      ;
    }

    /// <summary>
    /// Builds ordered tranfer history details by transaction time based on the requested account.
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="transferHistoryDetails"></param>
    /// <returns>List of transfer histories</returns>
    private List<TransferHistoryDetails> toTransferHistoryDetails(Guid accountId, IEnumerable<TransferHistory> transferHistoryDetails)
    {
      List<TransferHistoryDetails> accountTransferHistories = new List<TransferHistoryDetails>();
      foreach (var history in transferHistoryDetails)
      {
        var transferHistory = history.FromAccountId.Equals(accountId)
           ? new TransferHistoryDetails()
           {
             AccountId = history.ToAccountId,
             Amount = history.Amount * -1,
             TransactionTime = history.TransactionTime
           }
            : new TransferHistoryDetails()
            {
              AccountId = history.FromAccountId,
              Amount = history.Amount,
              TransactionTime = history.TransactionTime
            };
        accountTransferHistories.Add(transferHistory);
      }

      return accountTransferHistories.OrderBy(history => history.TransactionTime).ToList();
    }
  }
}