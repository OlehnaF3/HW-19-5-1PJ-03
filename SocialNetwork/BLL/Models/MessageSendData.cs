using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Models
{
    public class MessageSendData
    {
        public string Content { get; set; }

        public int SenderId { get; set; }

        public string EmailRecipient { get; set; }
    }
}
