namespace Bank.Business.Services
{
  using System.Threading.Tasks;
  using Bank.Business.Model;
  using Bank.Business.Models;
  using Bank.Shared;

  /// <summary>
  /// Adds an account.
  /// </summary>
  public interface IAddAccountToCustomerService : IAsyncService<Account, ServiceOutput<Null>>
  {
    // no-op
  }
}