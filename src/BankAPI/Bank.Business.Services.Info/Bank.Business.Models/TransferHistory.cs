namespace Bank.Business.Models
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  public sealed class TransferHistory
  {
    /// <summary>
    /// Id
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Account Id
    /// </summary>
    [Required]
    public Guid FromAccountId { get; set; }

    /// <summary>
    /// Account Id
    /// </summary>
    [Required]
    public Guid ToAccountId { get; set; }

    /// <summary>
    /// Account amount
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

      return obj is TransferHistory that
          && this.FromAccountId.Equals(that.FromAccountId) &&
           this.ToAccountId.Equals(that.ToAccountId) &&
           this.Amount == that.Amount &&
              this.TransactionTime.Equals(that.TransactionTime);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return HashCode.Combine(this.FromAccountId, this.ToAccountId, this.Amount, this.TransactionTime);
    }
  }
}