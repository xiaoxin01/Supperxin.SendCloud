using System;

namespace Supperxin.SendCloud
{
    public class MailAddress
    {
        public MailAddress(string address)
        {
            this.Address = address;
        }
        public MailAddress(string name, string address)
        {
            this.Name = name;
            this.Address = address;
        }

        public string Name { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Address))
            {
                throw new ArgumentNullException("Address can't be null!");
            }
            return string.IsNullOrWhiteSpace(Name) ?
                Address : $"{Name}<{Address}>";
        }
    }
}