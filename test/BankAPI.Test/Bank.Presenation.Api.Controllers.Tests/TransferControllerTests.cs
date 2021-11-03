namespace Bank.Presentation.Controllers.Tests
{
  using Xunit;
  using Moq;
  using Bank.Business.Services;
  using System;
  using Bank.Business.Models;
  using System.Threading.Tasks;
  using System.Collections.Generic;
  using Bank.Shared;
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;
  using System.Linq;

  public class TransferControllerTests : BaseTests
  {
    /// <summary>
    /// Mock add account service
    /// </summary>
    private readonly Mock<ITransferAmountService> mockTransferAmountService;

    /// <summary>
    /// Transfer amount service
    /// </summary>
    private readonly TransferController transferController;

    /// <summary>
    ///     Designated Constructor to instantiate mocks and class to be tested
    /// </summary>
    public TransferControllerTests()
    {
      this.mockRepository = new MockRepository(MockBehavior.Default);
      this.mockTransferAmountService = this.Create<ITransferAmountService>();

      this.transferController = new TransferController(
        this.mockTransferAmountService.Object);
      var mockHttpContext = mockRepository.Create<HttpContext>();
      var controllerContext = new ControllerContext();
      transferController.ControllerContext = controllerContext;
      controllerContext.HttpContext = mockHttpContext.Object;
    }

    [Fact]
    public async Task TransferAsyncSuccessTest()
    {
      // Arrange
      var fromAccountId = Guid.NewGuid();
      var toAccountId = Guid.NewGuid();
      var transfer = new Transfer()
      {
        Amount = 100,
        FromAccountId = fromAccountId,
        ToAccountId = toAccountId
      };
      this.mockTransferAmountService.Setup(x => x.ExecuteAsync(transfer)).ReturnsAsync(new ServiceOutput<Null>(null));

      // Act
      var actualResponse = await this.transferController.TransferAsync(transfer);

      // Assert
      Assert.True(actualResponse.Messages.Count() == 0);
      this.VerifyAll();
    }


    [Fact]
    public async Task TransferAsyncFailureTest()
    {
      // Arrange
      var fromAccountId = Guid.NewGuid();
      var toAccountId = Guid.NewGuid();
      var transfer = new Transfer()
      {
        Amount = 100,
        FromAccountId = fromAccountId,
        ToAccountId = toAccountId
      };
      var messages = new List<Message>() { (new Message("4", "account doesn't exist", MessageSeverity.Error)) };
      this.mockTransferAmountService.Setup(x => x.ExecuteAsync(transfer)).ReturnsAsync(new ServiceOutput<Null>(null, messages));

      // Act
      var actualResponse = await this.transferController.TransferAsync(transfer);

      // Assert
      Assert.Equal(new ApiDefaultResponse() { Messages = messages }, actualResponse);
      this.VerifyAll();
    }
  }
}