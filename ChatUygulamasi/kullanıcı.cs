using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatUygulamasi
{
    public class Kullanici
    {
        public Kullanici( string nick, string ip)
        {
            this.ip = ip;
            this.nick = nick;
        }
       // public string ConnectionId { get; set; }
        public string ip { get; set; }
        public string nick { get; set; }
    }
}