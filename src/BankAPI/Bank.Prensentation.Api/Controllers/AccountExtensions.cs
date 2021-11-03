namespace Bank.Presentation.Api.Controllers
{
  using Bank.Business.Models;
  using Bank.Presentation.Api.Contracts;

  /// <summary>
  ///  extensions for AccountInfo
  /// /// </summary>
  public static class AccountExtensions
  {
    /// <summary>
    ///     Converts to account details.
    /// </summary>
    /// <param name="accountInfo">account info</param>
    /// <returns>Account details</returns>
    public static AccountDetails ToAccountDetails(this AccountInfo accountInfo)
    {
      return accountInfo != null
      ? new AccountDetails()
      {
        Id = accountInfo.Id,
        Name = accountInfo.Name,
        Balance = accountInfo.Balance,
        Customer = new CustomerDetails()
        {
          Id = accountInfo.Customer.Id,
          Name = accountInfo.Customer.Name
        }
      }
      : null;
    }
  }
}