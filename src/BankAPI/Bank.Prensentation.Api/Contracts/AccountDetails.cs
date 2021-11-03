namespace Bank.Presentation.Api.Contracts
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  /// <summary>
  /// Account details
  /// </summary>
  public sealed class AccountDetails
  {
    /// <summary>
    ///     Account Id
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    ///     Account Name
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    ///     Account balance amount
    /// </summary>
    [Required]
    public double Balance { get; set; }

    /// <summary>
    ///     customer details
    /// </summary>
    [Required]
    public CustomerDetails Customer { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
      if (this == obj)
      {
        return true;
      }

      return obj is AccountDetails that
          && this.Id.Equals(that.Id) &&
              this.Name == that.Name &&
              this.Balance == that.Balance &&
              EqualityComparer<CustomerDetails>.Default.Equals(this.Customer, that.Customer);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return HashCode.Combine(this.Id, Name, this.Balance, this.Customer);
    }
  }

}