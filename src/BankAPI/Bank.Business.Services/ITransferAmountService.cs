namespace Bank.Business.Services
{
  using System.Threading.Tasks;
  using Bank.Business.Model;
  using Bank.Business.Models;
  using Bank.Shared;

  /// <summary>
  /// Transfers amount from one account  to another account.
  /// </summary>
  public interface ITransferAmountService : IAsyncService<Transfer, ServiceOutput<Null>>
  {
    // no-op
  }
}