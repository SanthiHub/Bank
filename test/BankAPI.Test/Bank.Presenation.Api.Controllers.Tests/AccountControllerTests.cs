namespace Bank.Presentation.Controllers.Tests
{
  using Xunit;
  using Moq;
  using Bank.Business.Services;
  using Bank.Business.Services.Info;
  using System;
  using Bank.Business.Models;
  using System.Threading.Tasks;
  using Bank.Presentation.Api.Contracts;
  using Bank.Presentation.Api.Controllers;
  using System.Collections.Generic;
  using Bank.Shared;
  using Bank.Business.Model;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Http;
  using System.Linq;

  public class AccountControllerTests : BaseTests
  {
    /// <summary>
    /// Mock add account service
    /// </summary>
    private readonly Mock<IAddAccountService> mockAddAccountService;

    /// <summary>
    /// Mock accounts details info service
    /// </summary>
    private readonly Mock<IAccountsDetailsInfoService> mockAccountsDetailsInfoService;

    /// <summary>
    /// Mock account details info service
    /// </summary>
    private readonly Mock<IAccountDetailsInfoService> mockAccountDetailsInfoService;

    /// <summary>
    /// Add account to customer
    /// </summary>
    private readonly Mock<IAddAccountToCustomerService> mockAddAccountToCustomerService;

    /// <summary>
    /// Account controller
    /// </summary>
    private readonly AccountController accountController;

    /// <summary>
    ///     Designated Constructor to instantiate mocks and class to be tested
    /// </summary>
    public AccountControllerTests()
    {
      this.mockRepository = new MockRepository(MockBehavior.Default);
      this.mockAddAccountService = this.Create<IAddAccountService>();
      this.mockAccountsDetailsInfoService = this.Create<IAccountsDetailsInfoService>();
      this.mockAccountDetailsInfoService = this.Create<IAccountDetailsInfoService>();
      this.mockAddAccountToCustomerService = this.Create<IAddAccountToCustomerService>();
      this.accountController = new AccountController(
        this.mockAddAccountService.Object,
        this.mockAccountDetailsInfoService.Object,
        this.mockAccountsDetailsInfoService.Object,
        this.mockAddAccountToCustomerService.Object);
      var httpContext = new DefaultHttpContext();
      var controllerContext = new ControllerContext();
      controllerContext.HttpContext = httpContext;
      accountController.ControllerContext = controllerContext;
    }

    [Fact]
    public async Task GetAccountTest()
    {
      // Arrange
      var accountId = Guid.NewGuid();
      var customerId = Guid.NewGuid();
      var accountInfo = new AccountInfo()
      {
        Id = accountId,
        Name = "Account1",
        Balance = 2345,
        Customer = new Customer()
        {
          Id = customerId,
          Name = "bankUser1"
        }
      };
      var expectedAccontDetails = new ApiResponse<AccountDetails>
      {
        Payload = new AccountDetails()
        {
          Id = accountId,
          Name = "Account1",
          Balance = accountInfo.Balance,
          Customer = new CustomerDetails()
          {
            Id = customerId,
            Name = "bankUser1"
          }
        }
      };
      this.mockAccountDetailsInfoService.Setup(x => x.ExecuteAsync(accountId)).ReturnsAsync(
        new ServiceOutput<AccountInfo>(accountInfo));

      // Act
      var actualAccontDetails = await this.accountController.GetAccountAsync(accountId);

      // Assert
      Assert.Equal(expectedAccontDetails.Payload, actualAccontDetails.Payload);
      this.VerifyAll();
    }

    [Fact]
    public async Task GetAccountInvalidAccountIdTest()
    {
      // Arrange
      var accountId = Guid.NewGuid();
      var messages = new List<Message>() { (new Message("4", "Account does't exist", MessageSeverity.Error)) };
      this.mockAccountDetailsInfoService.Setup(x => x.ExecuteAsync(accountId)).ReturnsAsync(
       new ServiceOutput<AccountInfo>(null, messages));
      var expectedAccontDetails = new ApiResponse<AccountDetails> { Messages = messages };

      // Act
      var actualAccontDetails = await this.accountController.GetAccountAsync(accountId);

      // Assert
      Assert.Equal(messages, actualAccontDetails.Messages);
      this.VerifyAll();
    }

    [Fact]
    public async Task GetAccountsTest()
    {
      // Arrange
      var accountId = Guid.NewGuid();
      var customerId = Guid.NewGuid();
      var accountInfo = new AccountInfo()
      {
        Id = accountId,
        Name = "Account1",
        Balance = 2345,
        Customer = new Customer()
        {
          Id = customerId,
          Name = "bankUser1"
        }
      };
      var expectedAccontDetailsOutput = new ApiResponse<IEnumerable<AccountDetails>>()
      {
        Payload = new List<AccountDetails>() {
         new AccountDetails()
          {
            Id = accountId,
             Name = "Account1",
            Balance = accountInfo.Balance,
            Customer = new CustomerDetails()
            {
              Id = customerId,
              Name = "bankUser1"
            }
          }
      }
      };

      this.mockAccountsDetailsInfoService.Setup(x => x.ExecuteAsync(null))
      .ReturnsAsync(new ServiceOutput<IEnumerable<AccountInfo>>(new List<AccountInfo>() { accountInfo }));

      // Act
      var actualAccountsDetails = await this.accountController.GetAccountsAsync();

      // Assert
      Assert.Equal(expectedAccontDetailsOutput.Payload, actualAccountsDetails.Payload);
      Assert.Equal(expectedAccontDetailsOutput.Messages, actualAccountsDetails.Messages);
      this.VerifyAll();
    }

    [Fact]
    public async Task CreateAsyncSuccessTest()
    {
      // Arrange
      var addAccountRequest = new AddAccountRequest()
      {
        Name = "Account1",
        Balance = 100,
        Customer = new CustomerInfo()
        {
          Name = "John Doe"
        }
      };

      this.mockAddAccountService.Setup(x => x.ExecuteAsync(new CustomerAccount()
      {
        Name = "Account1",
        Balance = 100,
        CustomerName = "John Doe"
      })).ReturnsAsync(new ServiceOutput<Null>(null));

      // Act
      var response = await this.accountController.CreateAsync(addAccountRequest);

      // Assert
      Assert.True(response.Messages.Count() == 0);
    }


    [Fact]
    public async Task CreateAsyncFailureTest()
    {
      // Arrange
      var addAccountRequest = new AddAccountRequest()
      {
        Name = "Account1",
        Balance = 100,
        Customer = new CustomerInfo()
        {
          Name = "John Doe"
        }
      };

      var messages = new List<Message>() { (new Message("4", "Can't create account", MessageSeverity.Error)) };
      this.mockAddAccountService.Setup(x => x.ExecuteAsync(new CustomerAccount()
      {
        Name = "Account1",
        Balance = 100,
        CustomerName = "John Doe"
      })).ReturnsAsync(new ServiceOutput<Null>(null, messages));

      // Act
      var response = await this.accountController.CreateAsync(addAccountRequest);

      // Assert
      Assert.Equal(new ApiDefaultResponse() { Messages = messages }, response);
    }

    [Fact]
    public async Task CreateAndLinkToCustomerAsyncSuccessTest()
    {
      // Arrange
      var customerId = Guid.NewGuid();
      var addAccountRequest = new AddAccountToCustomerRequest()
      {
        AccountName = "Account1",
        Balance = 100,
        CustomerId = customerId
      };

      this.mockAddAccountToCustomerService.Setup(x => x.ExecuteAsync(new Account()
      {
        Name = "Account1",
        Balance = 100,
        CustomerId = customerId
      })).ReturnsAsync(new ServiceOutput<Null>(null));

      // Act
      var response = await this.accountController.CreateAndLinkToCustomerAsync(addAccountRequest);

      // Assert
      Assert.True(response != null);

    }


    [Fact]
    public async Task CreateAndLinkToCustomerAsyncFailureTest()
    {
      // Arrange
      var customerId = Guid.NewGuid();
      var addAccountRequest = new AddAccountToCustomerRequest()
      {
        AccountName = "Account1",
        Balance = 100,
        CustomerId = customerId
      };

      var messages = new List<Message>() { (new Message("4", "Can't create account", MessageSeverity.Error)) };
      this.mockAddAccountToCustomerService.Setup(x => x.ExecuteAsync(new Account()
      {
        Name = "Account1",
        Balance = 100,
        CustomerId = customerId
      })).ReturnsAsync(new ServiceOutput<Null>(null, messages));

      // Act
      var response = await this.accountController.CreateAndLinkToCustomerAsync(addAccountRequest);

      // Assert
      Assert.Equal(new ApiDefaultResponse() { Messages = messages }, response);
    }
  }

}