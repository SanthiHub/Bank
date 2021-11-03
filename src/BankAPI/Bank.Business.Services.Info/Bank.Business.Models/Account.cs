namespace Bank.Business.Models
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  public sealed class Account
  {
    /// <summary>
    /// Account Id
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    ///     Account Name
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Account amount
    /// </summary>
    [Required]
    public double Balance { get; set; }

    /// <summary>
    /// customer id
    /// </summary>
    [ForeignKey("Customer")]
    [Required]
    public Guid CustomerId { get; set; }

    // <inheritdoc/>
    public override bool Equals(object obj)
    {
      if (this == obj)
      {
        return true;
      }

      return obj is Account that &&
              this.Id.Equals(that.Id) &&
              this.Balance == that.Balance &&
              this.Name == that.Name &&
              this.CustomerId.Equals(that.CustomerId);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return HashCode.Combine(this.Id, this.Name, this.Balance, this.CustomerId);
    }
  }
}