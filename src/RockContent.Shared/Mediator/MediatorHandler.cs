using System.Threading.Tasks;
using MediatR;
using RockContent.Shared.Commands;

namespace RockContent.Shared.Mediator
{
    public sealed class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CommandResult> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }
    }
}