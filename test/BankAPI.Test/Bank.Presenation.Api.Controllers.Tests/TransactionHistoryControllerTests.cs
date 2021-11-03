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
  using System.Collections.Generic;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Http;

  public class TransactionHistoryControllerTests : BaseTests
  {
    /// <summary>
    /// Mock add account service
    /// </summary>
    private readonly Mock<ITransferHistoryDetailsInfoService> mockTransferHistoryDetailsInfoService;

    /// <summary>
    /// Transfer amount service
    /// </summary>
    private readonly TransactionHistoryController transactionHistoryController;

    /// <summary>
    ///     Designated Constructor to instantiate mocks and class to be tested
    /// </summary>
    public TransactionHistoryControllerTests()
    {
      this.mockRepository = new MockRepository(MockBehavior.Default);
      this.mockTransferHistoryDetailsInfoService = this.Create<ITransferHistoryDetailsInfoService>();

      this.transactionHistoryController = new TransactionHistoryController(
        this.mockTransferHistoryDetailsInfoService.Object);
      var httpContext = new DefaultHttpContext();
      var controllerContext = new ControllerContext();
      controllerContext.HttpContext = httpContext;
      transactionHistoryController.ControllerContext = controllerContext;
    }

    [Fact]
    public async Task GetAsyncSuccessTestAsync()
    {

      // Arrange
      var accountId = Guid.NewGuid();
      var toAccountId = Guid.NewGuid();
      var transfer1 = new TransferHistory()
      {
        Amount = 100,
        FromAccountId = accountId,
        ToAccountId = toAccountId,
        TransactionTime = new DateTime(2021, 9, 3)

      };
      var transfer2 = new TransferHistory()
      {
        Amount = 50,
        FromAccountId = toAccountId,
        ToAccountId = accountId,
        TransactionTime = new DateTime(2021, 9, 23)
      };
      var accoutId3 = Guid.NewGuid();
      var transfer3 = new TransferHistory()
      {
        Amount = 1250,
        FromAccountId = accoutId3,
        ToAccountId = accountId,
        TransactionTime = new DateTime(2021, 9, 13)
      };
      var transferHistories = new List<TransferHistory>() { transfer1, transfer2, transfer3 };

      this.mockTransferHistoryDetailsInfoService.Setup(x => x.ExecuteAsync(accountId)).ReturnsAsync(new ServiceOutput<IEnumerable<TransferHistory>>(transferHistories));

      var transferHistoryDetails1 = new TransferHistoryDetails()
      {
        Amount = -100,
        AccountId = toAccountId,
        TransactionTime = new DateTime(2021, 9, 3)

      };
      var transferHistoryDetails2 = new TransferHistoryDetails()
      {
        Amount = 50,
        AccountId = toAccountId,
        TransactionTime = new DateTime(2021, 9, 23)
      };

      var transferHistoryDetails3 = new TransferHistoryDetails()
      {
        Amount = 1250,
        AccountId = accoutId3,
        TransactionTime = new DateTime(2021, 9, 13)
      };

      var expectedTransferHistories = new List<TransferHistoryDetails>() { transferHistoryDetails1, transferHistoryDetails3, transferHistoryDetails2 };

      // Act
      var actualTransactionHostories = await this.transactionHistoryController.GetAsync(accountId);

      // Assert
      Assert.Equal(expectedTransferHistories, actualTransactionHostories.Payload);
      this.VerifyAll();
    }
  }
}