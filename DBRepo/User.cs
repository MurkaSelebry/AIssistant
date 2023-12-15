using System;
using System.Collections.Generic;

namespace Social_Network.DBRepo
{
    public partial class User
    {
        public User()
        {
            Messages = new HashSet<Message>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
