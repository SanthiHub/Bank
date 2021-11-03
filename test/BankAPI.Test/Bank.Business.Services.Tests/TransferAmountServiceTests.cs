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

  public class TransferAmountServiceTests : BaseTests
  {
    /// <summary>
    /// Account repository
    /// </summary>/
    private readonly Mock<IAccountRepo> mockAccountRepository;

    /// <summary>
    /// Transfer history repository
    /// </summary>
    private readonly Mock<ITransferHistoryRepo> mockTransferHistoryRepo;

    /// <summary>
    /// Transfer account service
    /// </summary>
    private readonly TransferAmountService transferAmountService;

    /// <summary>
    ///     Designated Constructor to instantiate mocks and class to be tested
    /// </summary>
    public TransferAmountServiceTests()
    {
      this.mockRepository = new MockRepository(MockBehavior.Default);
      this.mockAccountRepository = this.Create<IAccountRepo>();
      this.mockTransferHistoryRepo = this.Create<ITransferHistoryRepo>();
      this.transferAmountService = new TransferAmountService(
        this.mockAccountRepository.Object,
        this.mockTransferHistoryRepo.Object);
    }

    [Fact]
    public async Task ExecuteAsyncSuccessTest()
    {
      // Arrrange
      var transfer = new Transfer()
      {
        FromAccountId = Guid.NewGuid(),
        ToAccountId = Guid.NewGuid(),
        Amount = 800
      };

      var fromAccount = new Account()
      {
        Name = "Account 1",
        Balance = 1000,
        CustomerId = Guid.NewGuid(),
        Id = transfer.FromAccountId
      };

      var toAccount = new Account()
      {
        Name = "Account 2",
        Balance = 100,
        CustomerId = Guid.NewGuid(),
        Id = transfer.ToAccountId
      };

      this.mockAccountRepository.Setup(x => x.GetAccountAsync(transfer.FromAccountId)).ReturnsAsync(fromAccount);
      this.mockAccountRepository.Setup(x => x.GetAccountAsync(transfer.ToAccountId)).ReturnsAsync(toAccount);

      this.mockAccountRepository.Setup(x => x.UpdateAccountAsync(new Account()
      {
        Name = "Account 1",
        Balance = 200,
        CustomerId = fromAccount.CustomerId,
        Id = transfer.FromAccountId
      })).Returns(Task.CompletedTask);

      this.mockAccountRepository.Setup(x => x.UpdateAccountAsync(new Account()
      {
        Name = "Account 2",
        Balance = 900,
        CustomerId = toAccount.CustomerId,
        Id = transfer.ToAccountId
      })).Returns(Task.CompletedTask);

      this.mockTransferHistoryRepo.Setup(x => x.CreateTransferHistoryAsync(new CreateTransferHistory()
      {
        FromAccountId = fromAccount.Id,
        ToAccountId = toAccount.Id,
        Amount = 800
      })).Returns(Task.CompletedTask);

      // Act
      var response = await this.transferAmountService.ExecuteAsync(transfer);

      Assert.Empty(response.Messages);
    }

    [Fact]
    public async Task ExecuteInvalidAccountTest()
    {
      // Arrrange
      var transfer = new Transfer()
      {
        FromAccountId = Guid.NewGuid(),
        ToAccountId = Guid.NewGuid(),
        Amount = 800
      };

      var toAccount = new Account()
      {
        Name = "Account 2",
        Balance = 100,
        CustomerId = Guid.NewGuid(),
        Id = transfer.ToAccountId
      };

      this.mockAccountRepository.Setup(x => x.GetAccountAsync(transfer.FromAccountId)).Returns(Task.FromResult<Account>(null));
      this.mockAccountRepository.Setup(x => x.GetAccountAsync(transfer.ToAccountId)).ReturnsAsync(toAccount);

      // Act
      var response = await this.transferAmountService.ExecuteAsync(transfer);

      Assert.Equal(new List<Message>() { new Message("3", "Please verify accounts", MessageSeverity.Error) }, response.Messages);
    }

    [Fact]
    public async Task ExecuteInvalidToAccountTest()
    {
      // Arrrange
      var transfer = new Transfer()
      {
        FromAccountId = Guid.NewGuid(),
        ToAccountId = Guid.NewGuid(),
        Amount = 800
      };

      var fromAccount = new Account()
      {
        Name = "Account 1",
        Balance = 1000,
        CustomerId = Guid.NewGuid(),
        Id = transfer.FromAccountId
      }; ;

      this.mockAccountRepository.Setup(x => x.GetAccountAsync(transfer.FromAccountId)).ReturnsAsync(fromAccount);
      this.mockAccountRepository.Setup(x => x.GetAccountAsync(transfer.ToAccountId)).Returns(Task.FromResult<Account>(null));

      // Act
      var response = await this.transferAmountService.ExecuteAsync(transfer);

      Assert.Equal(new List<Message>() { new Message("3", "Please verify accounts", MessageSeverity.Error) }, response.Messages);
    }


    [Fact]
    public async Task ExecuteInsufficientFundsTest()
    {
      // Arrrange
      var transfer = new Transfer()
      {
        FromAccountId = Guid.NewGuid(),
        ToAccountId = Guid.NewGuid(),
        Amount = 800
      };

      var fromAccount = new Account()
      {
        Name = "Account 1",
        Balance = 100,
        CustomerId = Guid.NewGuid(),
        Id = transfer.FromAccountId
      };

      var toAccount = new Account()
      {
        Name = "Account 2",
        Balance = 100,
        CustomerId = Guid.NewGuid(),
        Id = transfer.ToAccountId
      };

      this.mockAccountRepository.Setup(x => x.GetAccountAsync(transfer.FromAccountId)).ReturnsAsync(fromAccount);
      this.mockAccountRepository.Setup(x => x.GetAccountAsync(transfer.ToAccountId)).ReturnsAsync(toAccount);

      // Act
      var response = await this.transferAmountService.ExecuteAsync(transfer);

      Assert.Equal(
        new List<Message>() { new Message("4", "Account doesn't have sufficient funds", MessageSeverity.Error) },
        response.Messages);
    }

  }
}