namespace Bank.Presentation.Api.Contracts
{
  using System;
  using System.ComponentModel.DataAnnotations;

  public sealed class TransferDetails
  {
    /// <summary>
    /// Account Id
    /// </summary>
    [Required]
    public string FromAccountId { get; set; }

    /// <summary>
    /// Account Id
    /// </summary>
    [Required]
    public string ToAccountId { get; set; }

    /// <summary>
    /// Account amount
    /// </summary>
    [Required]
    public double Amount { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
      if (this == obj)
      {
        return true;
      }

      return obj is TransferDetails that
          && this.FromAccountId.Equals(that.FromAccountId) &&
           this.ToAccountId.Equals(that.ToAccountId) &&
              this.Amount == that.Amount;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return HashCode.Combine(this.FromAccountId, this.ToAccountId, this.Amount);
    }
  }
}