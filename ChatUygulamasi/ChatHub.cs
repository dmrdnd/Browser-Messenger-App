using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading;
using System.Net.NetworkInformation;//ping atmak için


namespace ChatUygulamasi
{
    public class ChatHub : Hub
    {
        static List<Kullanici> kullanıcılar = new List<Kullanici>();
        string istekip;
        string ip2, isim2;//ip ve isim bulma metodları için
        string kimdenisim;



        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            System.Threading.Thread.Sleep(15000);
            var item = kullanıcılar.FirstOrDefault(x => x.ip == Context.ConnectionId);
            if (item != null)
            {
                kullanıcılar.Remove(item);
            }

            Clients.All.online(kullanıcılar);
            return base.OnDisconnected(stopCalled);
        }


        public void baglan(string nick)
        {


            string Ip = Context.ConnectionId;

            kullanıcılar.Add(new Kullanici(nick, Ip));//servere kayıt
            Clients.All.online(kullanıcılar);//online yazdırır

        }

        public string isimbul(string ip)
        {
            for (int i = 0; i < kullanıcılar.Count; i++)
            {
                if (string.Compare(kullanıcılar[i].ip, ip) == 0)
                {
                    isim2 = kullanıcılar[i].nick;
                }
            }
            return isim2;
        }

        public string ip_bul(string isim)
        {

            for (int i = 0; i < kullanıcılar.Count; i++)
            {
                if (string.Compare(kullanıcılar[i].nick, isim) == 0)
                {
                    ip2 = kullanıcılar[i].ip;
                }
            }
            return ip2;
        }

        public void istekyolla(string kime)
        {

            istekip = ip_bul(kime);
            string kimden = Context.ConnectionId;

            kimdenisim = isimbul(kimden);
            Clients.Client(istekip).istekal(kimdenisim);

        }

        public void isteksonuc(String gonderenisim, int cevap)//istek yollayanın ekranına cvp gonder
        {
            string ipp = ip_bul(gonderenisim);
            if (cevap == 1)
            {
                Clients.Client(ipp).isteksonuc(1);
            }
            else if (cevap == 2)
            {
                Clients.Client(ipp).isteksonuc();
            }

        }

        public void Send(string username, string message, string kime)
        {
            string kimeIp = ip_bul(kime);
            Clients.Client(kimeIp).ekranamsjyaz(username, message);
            Clients.Client(Context.ConnectionId).ekranamsjyaz(username, message);

        }

        public void topluMesaj(string username, string mesaj)
        {
            Clients.All.ekranaToplumsjyaz(username, mesaj);
        }
        public void Disconnected()
        {
            var hduserid = Context.ConnectionId;
            Clients.Caller.stopClient(hduserid);
        }

    }
}