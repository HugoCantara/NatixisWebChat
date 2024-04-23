namespace NatixisWebChatDomain.AppHubs
{
    /// <summary>Class Message for hub</summary>
    public class MessageHub
    {
        /// <summary>The username that send the message</summary>
        public string? FromUserName { get; set; }

        /// <summary>The time that message send</summary>
        public string? Time { get; set; }

        /// <summary>The Message</summary>
        public string? Message { get; set; }
    }
}
