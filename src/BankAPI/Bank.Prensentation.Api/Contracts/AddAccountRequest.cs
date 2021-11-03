namespace Bank.Presentation.Api.Contracts
{
  using System;
  using System.ComponentModel.DataAnnotations;

  /// <summary>
  /// Add account request
  /// </summary>
  public sealed class AddAccountRequest
  {
    /// <summary>
    ///     Account Name
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    ///     Account balance
    /// </summary>
    [Required]
    public double Balance { get; set; }

    /// <summary>
    ///     Customer details
    /// </summary>
    [Required]
    public CustomerInfo Customer { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
      if (this == obj)
      {
        return true;
      }

      return obj is AddAccountRequest that
        &&
              this.Name.Equals(that.Name) &&
              this.Balance.Equals(that.Balance) &&
              this.Customer.Equals(that.Customer);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return HashCode.Combine(this.Name, this.Balance, this.Customer);
    }
  }
}