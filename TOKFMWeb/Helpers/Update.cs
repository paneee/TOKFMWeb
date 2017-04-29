using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace TOKFMWeb.Helpers
{
    public static class Update
    {
        public static void XML()
        {
            string path = (HostingEnvironment.MapPath(@"~/XMLDataFile/RSS.xml"));
            string url = "http://audycje.tokfm.pl/rss/a7c6a5012a556b";

            Rss rssDataWEB = new Rss();
            Rss rssDataXML = new Rss();

            rssDataWEB.GetFromWeb(url);
            rssDataXML.GetFromXML(path);
            rssDataWEB.Channel.AddItems(rssDataXML.Channel.Items);

            rssDataWEB.Channel.Title = "Wszystkie audycje TOKFM";
            rssDataWEB.Channel.Description = "TOKFM Update time: " + DateTime.Now + " (" + rssDataWEB.Channel.Items.Count + ")";
            rssDataWEB.Version = "2";
            rssDataWEB.Atom = "http://www.w3.org/2005/Atom";
            rssDataWEB.Itunes = "http://www.itunes.com/dtds/podcast-1.0.dtd";

            rssDataWEB.SaveToXML(path);
        }
    }
}