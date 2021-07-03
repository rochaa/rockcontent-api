using System.Threading.Tasks;
using RockContent.Shared.Commands;

namespace RockContent.Shared.Mediator
{
    public interface IMediatorHandler
    {
        Task<CommandResult> SendCommand<T>(T command) where T : Command;
    }
}
