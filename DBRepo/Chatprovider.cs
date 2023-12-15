using System;
using System.Collections.Generic;

namespace Social_Network.DBRepo
{
    public partial class Chatprovider
    {
        public Chatprovider()
        {
            Messages = new HashSet<Message>();
        }

        public int ProviderId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
