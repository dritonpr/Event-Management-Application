namespace EventManagement.Frontend.Services
{
    public class MessageService : IMessageService
    {
        private string _message;

        public void SetMessage(string message)
        {
            _message = message;
        }

        public string GetMessage()
        {
            var tempMessage = _message; // Store the current message
            _message = null; // Clear the message
            return tempMessage; // Return the stored message
        }
    }
    public interface IMessageService
    {
        void SetMessage(string message);
        string GetMessage();
    }
}
