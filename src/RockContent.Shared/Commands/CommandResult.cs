using System.Collections.Generic;
using Flunt.Notifications;

namespace RockContent.Shared.Commands
{
    public class CommandResult
    {
        public bool Sucess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public CommandResult(string message, IReadOnlyCollection<Notification> errors)
        {
            Sucess = false;
            Message = message;
            Data = errors.OnlyMessageErrors();
        }

        public CommandResult(string message, object data)
        {
            Sucess = true;
            Message = message;
            Data = data;
        }

        public CommandResult(string message)
        {
            Sucess = false;
            Message = message;
        }
    }
}