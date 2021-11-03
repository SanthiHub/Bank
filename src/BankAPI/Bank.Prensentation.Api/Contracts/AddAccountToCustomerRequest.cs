namespace Bank.Presentation.Api.Contracts
{
  using System;
  using System.ComponentModel.DataAnnotations;

  /// <summary>
  /// Add account request
  /// </summary>
  public sealed class AddAccountToCustomerRequest
  {
    /// <summary>
    ///     Account Name
    /// </summary>
    [Required]
    public string AccountName { get; set; }

    /// <summary>
    ///     Account balance
    /// </summary>
    [Required]
    public double Balance { get; set; }

    /// <summary>
    ///     Customer details
    /// </summary>
    [Required]
    public Guid CustomerId { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
      if (this == obj)
      {
        return true;
      }

      return obj is AddAccountToCustomerRequest that
        &&
              this.AccountName.Equals(that.AccountName) &&
              this.Balance == that.Balance &&
              this.CustomerId.Equals(that.CustomerId);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return HashCode.Combine(this.AccountName, this.Balance, this.CustomerId);
    }
  }
}