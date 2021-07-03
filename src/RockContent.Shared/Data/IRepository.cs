using System;
using RockContent.Shared.Entities;

namespace RockContent.Shared.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}