using Flunt.Notifications;
using MediatR;

namespace RockContent.Shared.Commands
{
    public abstract class Command : Notifiable<Notification>, IRequest<CommandResult>
    {
        public abstract void Validate();
    }
}