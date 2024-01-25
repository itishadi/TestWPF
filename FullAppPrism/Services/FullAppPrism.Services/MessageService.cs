using FullAppPrism.Services.Interfaces;

namespace FullAppPrism.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
