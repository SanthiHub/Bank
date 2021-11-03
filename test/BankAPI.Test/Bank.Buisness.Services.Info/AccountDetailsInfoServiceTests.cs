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

  public class AccountDetailsInfoServiceTests : BaseTests
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
    private readonly AccountDetailsInfoService accountDetailsInfoService;

    /// <summary>
    ///     Designated Constructor to instantiate mocks and class to be tested
    /// </summary>
    public AccountDetailsInfoServiceTests()
    {
      this.mockRepository = new MockRepository(MockBehavior.Default);
      this.mockAccountRepository = this.Create<IAccountRepo>();
      this.mockCustomerRepository = this.Create<ICustomerRepo>();
      this.accountDetailsInfoService = new AccountDetailsInfoService(
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

      var customer1 = new Customer()
      {
        Name = "Customer 1",
        Id = account1.CustomerId
      };

      this.mockAccountRepository.Setup(x => x.GetAccountAsync(account1.Id)).ReturnsAsync(account1);
      this.mockCustomerRepository.Setup(x => x.GetCustomerAsync(account1.CustomerId)).ReturnsAsync(customer1);
      var expectedAccount = new AccountInfo()
      {
        Name = "Account 1",
        Balance = 1000,
        Id = account1.Id,
        Customer = new Customer()
        {
          Name = "Customer 1",
          Id = account1.CustomerId
        }
      };

      // Act
      var response = await this.accountDetailsInfoService.ExecuteAsync(account1.Id);

      // Assert
      Assert.Equal(expectedAccount, response.Data);
    }

    [Fact]
    public async Task ExecuteInvalidAccountTest()
    {
      // Arrrange
      var account = new Account()
      {
        Name = "Account 1",
        Balance = 1000,
        CustomerId = Guid.NewGuid()
      };

      this.mockAccountRepository.Setup(x => x.GetAccountAsync(account.Id)).Returns(Task.FromResult<Account>(null));

      // Act
      var response = await this.accountDetailsInfoService.ExecuteAsync(account.Id);

      // Assert
      Assert.Equal(new ServiceOutput<AccountInfo>(
       null,
       new List<Message>() { new Message("4", "Account does't exist", MessageSeverity.Error) }), response);
    }
  }
}