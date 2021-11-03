namespace Bank.Presentation.Api.Controllers
{

  using System.Collections.Generic;
  using Bank.Business.Models;
  using Bank.Presentation.Api.Contracts;
  using System.Linq;
  using System;
  using Microsoft.AspNetCore.Mvc;
  using Bank.Business.Services.Info;
  using Bank.Business.Services;
  using Bank.Business.Model;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Http;
  using Bank.Shared;

  /// <summary>
  ///     Provides services related to get and create accounts.
  /// </summary>
  [Route("api/bank")]
  [ApiController]
  public sealed class AccountController : ControllerBase
  {
    /// <summary>
    /// Add account service
    /// </summary>
    private readonly IAddAccountService addAccountService;

    /// <summary>
    /// Accounts details info service
    /// </summary>
    private readonly IAccountsDetailsInfoService accountsDetailsInfoService;

    /// <summary>
    /// Account details info service
    /// </summary>
    private readonly IAccountDetailsInfoService accountDetailsInfoService;

    /// <summary>
    /// Add account to customer
    /// </summary>
    private readonly IAddAccountToCustomerService addAccountToCustomerService;

    /// <summary>
    /// Designated constructor
    /// </summary>
    /// <param name="addAccountService">Add account service</param>
    /// <param name="accountDetailsInfoService">Accounts details info service</param>
    /// <param name="accountsDetailsInfoService">Account details info service</param>
    public AccountController(
      IAddAccountService addAccountService,
      IAccountDetailsInfoService accountDetailsInfoService,
      IAccountsDetailsInfoService accountsDetailsInfoService,
      IAddAccountToCustomerService addAccountToCustomerService)
    {
      this.addAccountService = addAccountService;
      this.accountDetailsInfoService = accountDetailsInfoService;
      this.accountsDetailsInfoService = accountsDetailsInfoService;
      this.addAccountToCustomerService = addAccountToCustomerService;
    }

    /// <summary>
    /// Retrieves account information for the given account id
    /// </summary>
    /// <param name="accountId">Account id</param>
    /// <returns>Account information</returns>
    [HttpGet("account/{accountId}")]
    public async Task<ApiResponse<AccountDetails>> GetAccountAsync(Guid accountId)
    {
      var accountInfoOutput = await this.accountDetailsInfoService.ExecuteAsync(accountId);

      this.HttpContext.Response.StatusCode =
           accountInfoOutput.Messages.Any(m => m.Severity == MessageSeverity.Error) ?
            404 : 200;
      return new ApiResponse<AccountDetails>() { Payload = accountInfoOutput.Data?.ToAccountDetails(), Messages = accountInfoOutput.Messages };
    }

    /// <summary>
    /// Retrieves account information for the given account id
    /// </summary>
    /// <param name="accountId">Account id</param>
    /// <returns>Account information</returns>
    [HttpGet("{accountId}/balance")]
    public async Task<ApiResponse<double>> GetAccountBalanceAsync(Guid accountId)
    {
      var accountInfoOutput = await this.accountDetailsInfoService.ExecuteAsync(accountId);

      this.HttpContext.Response.StatusCode =
           accountInfoOutput.Messages.Any(m => m.Severity == MessageSeverity.Error) ? 404 : 200;
      return new ApiResponse<double>() { Payload = accountInfoOutput.Data.Balance, Messages = accountInfoOutput.Messages };
    }


    /// <summary>
    /// Retrieves all accounts information.
    /// </summary>
    /// <returns>Accounts information</returns>
    [HttpGet("accounts")]
    public async Task<ApiResponse<IEnumerable<AccountDetails>>> GetAccountsAsync()
    {
      var accountInfosOutput = await this.accountsDetailsInfoService.ExecuteAsync(null);

      this.HttpContext.Response.StatusCode =
      accountInfosOutput.Messages.Any(m => m.Severity == MessageSeverity.Error) ? 404 : 201;
      return new ApiResponse<IEnumerable<AccountDetails>>() { Payload = accountInfosOutput.Data.Select(account => account.ToAccountDetails()), Messages = accountInfosOutput.Messages };
    }

    /// <summary>
    /// Creates an account with customer information
    /// </summary>
    /// <param name="addAccountRequest">Account details</param>
    /// <returns>An awaitable task</returns> <summary>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost("account")]
    public async Task<ApiDefaultResponse> CreateAsync([FromBody] AddAccountRequest addAccountRequest)
    {
      var addAccountsResponse = await this.addAccountService.ExecuteAsync(new CustomerAccount()
      {
        Name = addAccountRequest.Name,
        Balance = addAccountRequest.Balance,
        CustomerName = addAccountRequest.Customer.Name,

      });

      // Set response status code based on messages
      this.HttpContext.Response.StatusCode =
      addAccountsResponse.Messages.Any(m => m.Severity == MessageSeverity.Error) ? 401 : 201;

      return new ApiDefaultResponse() { Messages = addAccountsResponse.Messages };
    }


    /// <summary>
    /// Creates an account with existing customer information
    /// </summary>
    /// <param name="addAccountToCustomerRequest">Account details</param>
    /// <returns>task</returns> <summary>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost("linkAccount")]
    public async Task<ApiDefaultResponse> CreateAndLinkToCustomerAsync([FromBody] AddAccountToCustomerRequest addAccountToCustomerRequest)
    {
      var addAccountToCustomerResponse = await this.addAccountToCustomerService.ExecuteAsync(new Account()
      {
        Name = addAccountToCustomerRequest.AccountName,
        Balance = addAccountToCustomerRequest.Balance,
        CustomerId = addAccountToCustomerRequest.CustomerId
      });

      this.HttpContext.Response.StatusCode =
          addAccountToCustomerResponse.Messages.Any(m => m.Severity == MessageSeverity.Error) ? 401 : 201;

      return new ApiDefaultResponse() { Messages = addAccountToCustomerResponse.Messages };
    }
  }
}