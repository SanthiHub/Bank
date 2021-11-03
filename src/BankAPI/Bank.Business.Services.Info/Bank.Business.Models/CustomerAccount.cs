namespace Bank.Business.Model
{
  using System;
  using System.ComponentModel.DataAnnotations;

  public sealed class CustomerAccount
  {
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
    ///     Customer name
    /// </summary>
    public string CustomerName { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
      if (this == obj)
      {
        return true;
      }

      return obj is CustomerAccount that
            &&
              this.Name == that.Name &&
              this.Balance == that.Balance &&
               this.CustomerName == that.CustomerName;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return HashCode.Combine(this.Name, this.Balance, this.CustomerName);
    }
  }
}