using System;
using System.Collections.Generic;

namespace Bank.Shared
{
  /// <summary>
  ///  Response details
  /// </summary>
  public class ApiResponse<T>
  {
    public T Payload { get; set; }
    public IEnumerable<Message> Messages { get; set; } = new List<Message>();

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
      if (this == obj)
      {
        return true;
      }

      return obj is ApiResponse<T> that
        && EqualityComparer<IEnumerable<Message>>.Default.Equals(Messages, that.Messages) &&
        EqualityComparer<T>.Default.Equals(Payload, that.Payload);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return HashCode.Combine(this.Payload, this.Messages);
    }
  }

}
