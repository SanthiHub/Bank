namespace Bank.Business.Services
{
  using Bank.Business.Model;
  using Bank.Shared;

  /// <summary>
  /// Adds an account.
  /// </summary>
  public interface IAddAccountService : IAsyncService<CustomerAccount, ServiceOutput<Null>>
  {
    // no-op
  }
}