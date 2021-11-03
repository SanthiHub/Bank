using System;
using System.Collections.Generic;
using Bank.Shared;

namespace Bank.Business.Services
{
  public sealed class ServiceOutput<T>
  {
    public ServiceOutput(T Data, IEnumerable<Message> messages = null)
    {
      this.Data = Data;
      this.Messages = messages ?? new List<Message>();
    }

    public T Data { get; }
    public IEnumerable<Message> Messages { get; }

    public override bool Equals(object obj)
    {
      return obj is ServiceOutput<T> output &&
             EqualityComparer<T>.Default.Equals(Data, output.Data);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(Data);
    }
  }

}