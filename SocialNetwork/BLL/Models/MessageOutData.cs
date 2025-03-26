using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Models
{
    public class MessageOutData
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Recipient { get; set; }

        public string Sender { get; set; }
    }
}
