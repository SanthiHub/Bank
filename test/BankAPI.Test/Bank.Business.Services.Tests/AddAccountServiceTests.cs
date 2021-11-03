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

  public class AddAccountServiceTests : BaseTests
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
    private readonly AddAccountService addAccountService;

    /// <summary>
    ///     Designated Constructor to instantiate mocks and class to be tested
    /// </summary>
    public AddAccountServiceTests()
    {
      this.mockRepository = new MockRepository(MockBehavior.Default);
      this.mockAccountRepository = this.Create<IAccountRepo>();
      this.mockCustomerRepository = this.Create<ICustomerRepo>();
      this.addAccountService = new AddAccountService(
        this.mockAccountRepository.Object,
        this.mockCustomerRepository.Object);
    }



    [Fact]
    public async Task ExecuteCustomerExistsErrorAsyncTest()
    {
      // Arrrange
      var customers = new List<Customer>() {
          new Customer() {
                Name = "Account1",
                Id = Guid.NewGuid()
          },
         new Customer() {
               Name = "Account2",
                Id = Guid.NewGuid()
          }
        };

      this.mockCustomerRepository.Setup(x => x.GetCustomersAsync()).ReturnsAsync(customers);
      // Act
      var customerAccount = new CustomerAccount()
      {
        Balance = 500,
        Name = "New Account",
        CustomerName = "Account2"
      };
      var response = await this.addAccountService.ExecuteAsync(customerAccount);

      // Assert
      Assert.Equal(new List<Message>() {
         new Message("1", "Customer Already exists. Please select another customer or use link account API", MessageSeverity.Error) }, response.Messages);
    }

    [Fact]
    public async Task ExecuteAsyncSuccessTest()
    {
      // Arrrange
      var customers = new List<Customer>() {
          new Customer() {
                Name = "Account1",
                Id = Guid.NewGuid()
          },
         new Customer() {
               Name = "Account2",
                Id = Guid.NewGuid()
          }
        };

      this.mockCustomerRepository.Setup(x => x.GetCustomersAsync()).ReturnsAsync(customers);
      var customerId = Guid.NewGuid();
      this.mockCustomerRepository
        .Setup(x => x.CreateCustomerAsync(new CreateCustomer() { Name = "New Customer" })).ReturnsAsync(customerId);
      this.mockAccountRepository.Setup(x => x.CreateAccountAsync(new CreateAccount()
      {
        Name = "New Account",
        Balance = 500,
        CustomerId = customerId
      })).Returns(Task.CompletedTask);

      // Act
      var customerAccount = new CustomerAccount()
      {
        Balance = 500,
        Name = "New Account",
        CustomerName = "New Customer"
      };
      var response = await this.addAccountService.ExecuteAsync(customerAccount);

      // Assert
      Assert.Empty(response.Messages);
    }
  }

}