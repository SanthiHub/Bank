namespace Bank.Presentation.Api.Contracts
{
  using System;
  using System.ComponentModel.DataAnnotations;

  public sealed class CustomerDetails
  {

    /// <summary>
    ///     Customer Id
    /// </summary>
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

      return obj is CustomerDetails that
       && this.Id.Equals(that.Id)
          && this.Name == that.Name;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return HashCode.Combine(this.Id, this.Name);
    }
  }
}