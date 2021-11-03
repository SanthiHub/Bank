using System;
using Moq;

namespace Bank.Presentation.Controllers.Tests
{
  /// <summary>
  ///     Base class for all unit tests.
  /// </summary>
  public abstract class BaseTests
  {
    /// <summary>
    ///  Mock repository.
    /// </summary>
    public MockRepository mockRepository;


    /// <summary>
    ///     Verifies all the mocks created/registered with this repository.
    /// </summary>
    public void VerifyAll()
    {
      this.mockRepository.VerifyAll();
    }

    /// <summary>
    ///  Creates a mock and registers it with this repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="mockBehavior"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public Mock<T> Create<T>(MockBehavior mockBehavior = MockBehavior.Strict, params object[] args)
    where T : class
    {
      return this.mockRepository.Create<T>(mockBehavior, args);
    }
  }
}