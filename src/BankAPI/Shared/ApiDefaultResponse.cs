using System;
using System.Collections.Generic;

namespace Bank.Shared
{
  /// <summary>
  ///  Response details
  /// </summary>
  public class ApiDefaultResponse
  {

    public IEnumerable<Message> Messages { get; set; } = new List<Message>() { };

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
      if (this == obj)
      {
        return true;
      }

      return obj is ApiDefaultResponse that
        && this.Messages.Equals(that.Messages);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return HashCode.Combine(this.Messages);
    }

  }

}
