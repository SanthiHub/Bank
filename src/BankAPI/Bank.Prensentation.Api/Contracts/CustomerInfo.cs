namespace Bank.Presentation.Api.Contracts
{
  using System;
  using System.ComponentModel.DataAnnotations;

  public sealed class CustomerInfo
  {

    /// <summary>
    ///     Customer name
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
      if (this == obj)
      {
        return true;
      }

      return obj is CustomerInfo that
           && this.Name.Equals(that.Name);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return HashCode.Combine(this.Name);
    }
  }
}