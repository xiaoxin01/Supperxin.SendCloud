using System.Collections.Generic;

namespace Supperxin.SendCloud
{
    public class SendCloudMessage
    {
        public SendCloudMessage()
        {
            this.To = new List<MailAddress>();
            this.Parameters = new List<Parameter>();
        }
        public MailAddress From { get; set; }
        public List<MailAddress> To { get; set; }
        public string Subject { get; set; }
        public string Html { get; set; }
        public string Plain { get; set; }
        public List<Parameter> Parameters { get; set; }
    }
}