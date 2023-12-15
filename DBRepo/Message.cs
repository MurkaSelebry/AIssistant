using System;
using System.Collections.Generic;

namespace Social_Network.DBRepo
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public int ChatProviderId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime? SentAt { get; set; }

        public virtual Chatprovider ChatProvider { get; set; } = null!;
        public virtual User Sender { get; set; } = null!;
    }
}
