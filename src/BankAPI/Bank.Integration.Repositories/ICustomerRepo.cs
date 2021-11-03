namespace Bank.Integration.Repositories
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Bank.Business.Models;

  /// <summary>
  ///Operations related customer repository
  public interface ICustomerRepo
  /// </summary>
  {
    Task<Guid> CreateCustomerAsync(CreateCustomer customer);

    Task<Customer> GetCustomerAsync(Guid id);

    Task<IEnumerable<Customer>> GetCustomersAsync();
  }
}