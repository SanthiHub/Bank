namespace Bank.Integration.Repositories
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Bank.Business.Models;
  using Microsoft.EntityFrameworkCore;

  /// <summary>
  ///     Customer repository
  /// </summary>
  public sealed class CustomerRepo : ICustomerRepo
  {

    /// <summary>
    /// Bank Data base context
    /// </summary>
    private readonly BankAppDbContext appDbContext;

    /// <summary>
    /// Designated constructor
    /// </summary>
    /// <param name="appDbContext">App database context</param>
    public CustomerRepo(BankAppDbContext appDbContext)
    {
      this.appDbContext = appDbContext;
    }

    /// <summary>
    /// Creates Customer
    /// </summary>
    /// <param name="createCustomer">Customer details</param>
    /// <returns>Customer id</returns>
    public async Task<Guid> CreateCustomerAsync(CreateCustomer createCustomer)
    {
      var customer = new Customer() { Name = createCustomer.Name };
      await this.appDbContext.AddAsync(customer);
      this.appDbContext.SaveChanges();
      return customer.Id;
    }

    /// <summary>
    /// Retrieves cistomer
    /// </summary>
    /// <param name="id">customer id</param>
    /// <returns>Customer details</returns>
    public async Task<Customer> GetCustomerAsync(Guid id)
    {
      return await this.appDbContext.Customers.FirstOrDefaultAsync(Customer => Customer.Id.Equals(id));
    }

    /// <summary>
    ///  List of all customers
    /// </summary>
    /// <returns>List of customers</returns>
    public async Task<IEnumerable<Customer>> GetCustomersAsync()
    {
      return await appDbContext.Customers.ToListAsync();
    }
  }
}