namespace Bank.Business.Services.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Bank.Business.Model;
  using Bank.Business.Models;
  using Bank.Business.Services.Info;
  using Bank.Integration.Repositories;
  using Bank.Presentation.Controllers.Tests;
  using Bank.Shared;
  using Moq;
  using Xunit;

  public class AccountsDetailsInfoServiceTests : BaseTests
  {
    /// <summary>
    /// Account repository
    /// </summary>/
    private readonly Mock<IAccountRepo> mockAccountRepository;

    /// <summary>
    /// Customer repository
    /// </summary>
    private readonly Mock<ICustomerRepo> mockCustomerRepository;

    /// <summary>
    /// Add account service
    /// </summary>
    private readonly AccountsDetailsInfoService accountsDetailsInfoService;

    /// <summary>
    ///     Designated Constructor to instantiate mocks and class to be tested
    /// </summary>
    public AccountsDetailsInfoServiceTests()
    {
      this.mockRepository = new MockRepository(MockBehavior.Default);
      this.mockAccountRepository = this.Create<IAccountRepo>();
      this.mockCustomerRepository = this.Create<ICustomerRepo>();
      this.accountsDetailsInfoService = new AccountsDetailsInfoService(
        this.mockAccountRepository.Object,
        this.mockCustomerRepository.Object);
    }

    [Fact]
    public async Task ExecuteAsyncTest()
    {
      // Arrrange
      var account1 = new Account()
      {
        Name = "Account 1",
        Balance = 1000,
        CustomerId = Guid.NewGuid()
      };

      var account2 = new Account()
      {
        Name = "Account 2",
        Balance = 100,
        CustomerId = Guid.NewGuid()
      };

      var customer1 = new Customer()
      {
        Name = "Customer 1",
        Id = account1.CustomerId
      };


      var customer2 = new Customer()
      {
        Name = "Customer 2",
        Id = account2.CustomerId
      };

      this.mockAccountRepository.Setup(x => x.GetAccountsAsync()).ReturnsAsync(new List<Account>() { account1, account2 });

      this.mockCustomerRepository.Setup(x => x.GetCustomerAsync(account1.CustomerId)).ReturnsAsync(customer1);
      this.mockCustomerRepository.Setup(x => x.GetCustomerAsync(account2.CustomerId)).ReturnsAsync(customer2);
      var expectedAccounts = new List<AccountInfo>()
      {
         new AccountInfo(){
             Name = "Account 1",
             Balance = 1000,
             Id = account1.Id,
             Customer = new Customer() {
              Name = "Customer 1",
              Id = account1.CustomerId
              }
         },
          new AccountInfo(){
             Name = "Account 2",
             Balance = 100,
              Id = account2.Id,
             Customer = new Customer() {
              Name = "Customer 2",
              Id = account2.CustomerId
              }
         }};

      // Act
      var response = await this.accountsDetailsInfoService.ExecuteAsync(null);

      // Assert
      Assert.Equal(expectedAccounts, response.Data);
    }
  }
}