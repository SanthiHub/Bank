namespace Bank.Presentation.Api.Contracts
{
  using System;
  using System.ComponentModel.DataAnnotations;

  public sealed class TransferHistoryDetails
  {
    /// <summary>
    /// Account Id
    /// </summary>
    [Required]
    public Guid AccountId { get; set; }

    /// <summary>
    /// amount
    /// </summary>
    [Required]
    public double Amount { get; set; }

    /// <summary>
    /// Account amount
    /// </summary>
    [Required]
    public DateTime TransactionTime { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
      if (this == obj)
      {
        return true;
      }

      return obj is TransferHistoryDetails that
          && this.AccountId.Equals(that.AccountId) &&
             this.Amount == that.Amount &&
             this.TransactionTime == that.TransactionTime;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return HashCode.Combine(this.AccountId, this.Amount, this.TransactionTime);
    }
  }
}