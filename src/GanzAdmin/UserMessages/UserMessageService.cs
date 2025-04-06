using System;

namespace GanzAdmin.UserMessages
{
    public class UserMessageService
    {
        public event EventHandler<UserMessage> UserMessageQueued;

        public void QueueMessage(UserMessage message)
        {
            UserMessageQueued?.Invoke(this, message);
        }
    }
}
