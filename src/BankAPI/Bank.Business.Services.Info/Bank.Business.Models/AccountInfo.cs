namespace Bank.Business.Models
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  public sealed class AccountInfo
  {
    /// <summary>
    /// Account Id
    /// </summary>
    [Key]
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Account name
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Account amount
    /// </summary>
    [Required]
    public double Balance { get; set; }

    /// <summary>
    ///     customer details
    /// </summary>
    [Required]
    public Customer Customer { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
      if (this == obj)
      {
        return true;
      }

      return obj is AccountInfo that
          && this.Id.Equals(that.Id) &&
              this.Name == that.Name &&
              this.Balance == that.Balance &&
              EqualityComparer<Customer>.Default.Equals(this.Customer, that.Customer);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return HashCode.Combine(this.Id, this.Name, this.Balance, this.Customer);
    }
  }
}