namespace Bank.Presentation.Api.Contracts
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using Bank.Business.Models;
  public sealed class CustomerAccountsDetails
  {
    /// <summary>
    ///  Customer Id
    /// </summary>
    [Required]
    public string Id { get; set; }

    /// <summary>
    /// Customer name
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    ///  List of Accounts
    /// </summary>
    [Required]
    public IEnumerable<Account> Accounts { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
      if (this == obj)
      {
        return true;
      }

      return obj is CustomerAccountsDetails that
          &&
              this.Id.Equals(that.Id) &&
              this.Name.Equals(that.Name) &&
              this.Accounts.Equals(that.Accounts);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return HashCode.Combine(this.Id, this.Name, this.Accounts);
    }
  }

}