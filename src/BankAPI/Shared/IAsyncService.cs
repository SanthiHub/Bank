using System.Threading.Tasks;

namespace Bank.Shared
{
  /// <summary>
  ///     Generic service interface.
  /// </summary>
  /// <typeparam name="TInput">type of the service input</typeparam>
  /// <typeparam name="TOutput">type of the service output</typeparam>
  public interface IAsyncService<in TInput, TOutput>
  {
    /// <summary>
    ///     This method executes the service.
    /// </summary>
    /// <param name="input">service input</param>
    /// <returns>An awaitable task with result of type <typeparamref name="TOutput"/></returns>
    Task<TOutput> ExecuteAsync(TInput input);
  }
}