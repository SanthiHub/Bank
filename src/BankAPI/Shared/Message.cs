namespace Bank.Shared
{
  using System;
  public sealed class Message
  {

    /// <summary>
    /// Designated constructor
    /// </summary>
    /// <param name="id">The id of the message.</param>
    /// <param name="text">Message details</param>
    /// <param name="severity">Message severity.</param>
    public Message(string id, string text, MessageSeverity severity)
    {
      this.Id = id;
      this.Text = text;
      this.Severity = severity;
    }

    /// <summary>
    /// The id of the message.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Message details
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// Message severity.
    /// </summary>
    public MessageSeverity Severity { get; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
      if (this == obj)
      {
        return true;
      }

      return obj is Message that
        &&
              this.Id.Equals(that.Id) &&
              this.Text == that.Text &&
              this.Severity == that.Severity;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return HashCode.Combine(this.Id, this.Text, this.Severity);
    }
  }
}