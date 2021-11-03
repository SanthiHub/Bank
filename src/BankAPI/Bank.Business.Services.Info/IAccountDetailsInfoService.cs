using System;
using System.Threading.Tasks;
using Bank.Business.Models;
using Bank.Business.Services.Info;
using Bank.Shared;

namespace Bank.Business.Services.Info
{
  /// <summary>
  /// Retrieves account information.
  /// </summary>
  public interface IAccountDetailsInfoService : IAsyncService<Guid, ServiceOutput<AccountInfo>>
  {
    // no-op
  }
}