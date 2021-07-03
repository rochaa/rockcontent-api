
using System.Collections.Generic;

namespace Flunt.Notifications
{
    public static class NotifiableExtends
    {
        public static IReadOnlyCollection<string> OnlyMessageErrors(this IReadOnlyCollection<Notification> notifications)
        {
            var errors = new List<string>();
            foreach (var notification in notifications)
                errors.Add(notification.Message);

            return (IReadOnlyCollection<string>)errors;
        }
    }
}