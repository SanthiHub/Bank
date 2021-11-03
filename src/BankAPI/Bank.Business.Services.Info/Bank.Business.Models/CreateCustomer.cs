namespace Bank.Business.Models
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  public sealed class CreateCustomer
  {
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

      return obj is CreateCustomer that
          && this.Name == that.Name;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return HashCode.Combine(this.Name);
    }
  }
}