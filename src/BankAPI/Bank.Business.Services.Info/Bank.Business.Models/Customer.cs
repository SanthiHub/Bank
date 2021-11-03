namespace Bank.Business.Models
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  public sealed class Customer
  {

    /// <summary>
    ///     Customer Id
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    ///     Customer name
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
      if (this == obj)
      {
        return true;
      }

      return obj is Customer that
          && this.Id.Equals(that.Id) &&
              this.Name == that.Name;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return HashCode.Combine(this.Id, this.Name);
    }
  }
}