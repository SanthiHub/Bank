namespace Bank.Presentation.Controllers
{

  using Bank.Business.Models;
  using Microsoft.AspNetCore.Mvc;
  using Bank.Business.Services;
  using System.Threading.Tasks;
  using Bank.Shared;

  /// <summary>
  /// Provides services related to amount transafer between accounts.
  /// </summary>
  [Route("api/bank")]
  [ApiController]
  public sealed class TransferController : ControllerBase
  {
    /// <summary>
    /// Transfer amount service
    /// </summary>
    private readonly ITransferAmountService transferAmountService;

    /// <summary>
    /// Designated constructor
    /// </summary>
    /// <param name="transferAmountService">Transfer amount service</param>
    public TransferController(
    ITransferAmountService transferAmountService)
    {
      this.transferAmountService = transferAmountService;
    }

    /// <summary>
    /// Transfers amount from one account to another account
    /// </summary>
    /// <param name="transferDetails">Account transfer deatils</param>
    /// <returns>task</returns>
    [HttpPost("transfer")]
    public async Task<ApiDefaultResponse> TransferAsync([FromBody] Transfer transferDetails)
    {
      var transferAmountResponse = await this.transferAmountService.ExecuteAsync(transferDetails);
      return new ApiDefaultResponse() { Messages = transferAmountResponse.Messages };
    }
  }
}