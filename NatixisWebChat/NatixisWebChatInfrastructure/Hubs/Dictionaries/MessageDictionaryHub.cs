namespace NatixisWebChatInfrastructure.Hubs.Dictionaries
{
    using NatixisWebChatDomain.AppHubs;

    /// <summary>Class GroupDictionaryHub</summary>
    public class MessageDictionaryHub
    {
        /// <summary>Message hub dictionary</summary>
        public Dictionary<string, List<MessageHub>> dictionary = new Dictionary<string, List<MessageHub>>();

        /// <summary>Add message to dictionary</summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(string key, MessageHub value)
        {
            if (this.dictionary.ContainsKey(key))
            {
                List<MessageHub> list = this.dictionary[key];
                if (list.Count < 10)
                {
                    list.Add(value);
                }
                else
                {
                    list.RemoveAt(0);
                    list.Add(value);
                }
            }
            else
            {
                var list = new List<MessageHub> { value };
                this.dictionary.Add(key, list);
            }
        }

        /// <summary>Get last message list</summary>
        /// <param name="key">The key.</param>
        /// <returns>List<MessageHub>?</returns>
        public List<MessageHub>? GetLastMessageList(string key)
        {
            if (this.dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            else
            {
                return null;
            }
        }
    }
}
