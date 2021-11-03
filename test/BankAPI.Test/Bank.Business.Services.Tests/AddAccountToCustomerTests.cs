namespace Bank.Business.Services.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Bank.Business.Model;
  using Bank.Business.Models;
  using Bank.Integration.Repositories;
  using Bank.Presentation.Controllers.Tests;
  using Bank.Shared;
  using Moq;
  using Xunit;

  public class AddAccountToCustomerTests : BaseTests
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
    private readonly AddAccountToCustomerService addAccountToCustomerService;

    /// <summary>
    ///     Designated Constructor to instantiate mocks and class to be tested
    /// </summary>
    public AddAccountToCustomerTests()
    {
      this.mockRepository = new MockRepository(MockBehavior.Default);
      this.mockAccountRepository = this.Create<IAccountRepo>();
      this.mockCustomerRepository = this.Create<ICustomerRepo>();
      this.addAccountToCustomerService = new AddAccountToCustomerService(
        this.mockAccountRepository.Object,
        this.mockCustomerRepository.Object);
    }

    [Fact]
    public async Task ExecuteAsyncSuccessTest()
    {
      // Arrrange
      var accountToAdd = new Account()
      {
        Balance = 500,
        CustomerId = Guid.NewGuid(),
        Name = "Link Account"
      };


      this.mockCustomerRepository.Setup(x => x.GetCustomerAsync(accountToAdd.CustomerId)).ReturnsAsync(new Customer()
      {
        Name = "User2",
        Id = accountToAdd.CustomerId
      });
      this.mockAccountRepository.Setup(x => x.CreateAccountAsync(new CreateAccount()
      {
        Name = "Link Account",
        Balance = 500,
        CustomerId = accountToAdd.CustomerId
      })).Returns(Task.CompletedTask);

      // Act

      var response = await this.addAccountToCustomerService.ExecuteAsync(accountToAdd);

      // Assert
      Assert.Empty(response.Messages);
    }

    [Fact]
    public async Task ExecuteInvalidCustomerTest()
    {
      // Arrrange
      var accountToAdd = new Account()
      {
        Balance = 500,
        CustomerId = Guid.NewGuid(),
        Name = "Link Account"
      };


      this.mockCustomerRepository.Setup(x => x.GetCustomerAsync(accountToAdd.CustomerId)).Returns(Task.FromResult<Customer>(null));
      this.mockAccountRepository.Setup(x => x.CreateAccountAsync(new CreateAccount()
      {
        Name = "Link Account",
        Balance = 500,
        CustomerId = accountToAdd.CustomerId
      })).Returns(Task.CompletedTask);

      // Act
      var response = await this.addAccountToCustomerService.ExecuteAsync(accountToAdd);

      // Assert
      Assert.Equal(new List<Message>() { new Message("2", "Customer doesn't Exist", MessageSeverity.Error) }, response.Messages);
    }
  }
}



