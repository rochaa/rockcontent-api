using MediatR;

namespace RockContent.Shared.Commands
{
    public interface ICommandHandler<T> : IRequestHandler<T, CommandResult> where T : Command { }
}