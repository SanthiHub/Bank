using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Business.Models;
using Bank.Business.Services.Info;
using Bank.Shared;

namespace Bank.Business.Services.Info
{
  /// <summary>
  /// Retrieves all accounts information.
  /// </summary>
  public interface IAccountsDetailsInfoService : IAsyncService<Null, ServiceOutput<IEnumerable<AccountInfo>>>
  {
    // no-op
  }
}