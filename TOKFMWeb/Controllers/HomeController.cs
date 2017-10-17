using MoreLinq;
using NLog;
using SimpleFeedReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using TOKFMWeb.Helpers;
using TOKFMWeb.Models;

namespace TOKFMWeb.Controllers
{
    public class HomeController : Controller
    {
        private string path { get; set; }
        private string path2 { get; set; }
        public HomeController()
        {
            path = HostingEnvironment.MapPath(@"~/XMLDataFile/RSS.xml");
            path2 = HostingEnvironment.MapPath(@"~/XMLDataFile/OMPL.xml");
        }

        public ActionResult Index()
        {
            Rss rssXML = new Rss();
            rssXML.GetFromXML(path);
            LogManager.GetCurrentClassLogger().Info("IP: " + HttpContext.Request.UserHostAddress);
            return View(rssXML);
        }

        public ActionResult Rss(string id)
        {
            Rss rssDataXML = new Rss();
            rssDataXML.GetFromXML(path);
            rssDataXML.Atom = "http://www.w3.org/2005/Atom";
            rssDataXML.Itunes = "http://www.itunes.com/dtds/podcast-1.0.dtd";


            if (id != null)
            {
                rssDataXML.Channel.Items.RemoveAll(p => !p.Image2.Href.Contains(id));
                try
                {
                    Item item1 = new Item();
                    item1 = rssDataXML.Channel.Items.Where(p => p.Image2.Href.Contains(id)).FirstOrDefault();
                    rssDataXML.Channel.Image.Url = item1.Image2.Href;
                }
                catch
                {

                }
            }

            try
            {
                var sw = new System.IO.StringWriter();
                var serializer = new XmlSerializer(typeof(Rss));
                serializer.Serialize(sw, rssDataXML);

                string xmlString = sw.ToString();
                xmlString = xmlString.Replace("http://serwisy.gazeta.pl/i/33/tokfm/logoTokFm600.jpg", rssDataXML.Channel.Image.Url).
                            Replace("Ostatnio dodane - Radio TOK FM", "TOK FM").
                            Replace("Wszystkie audycje TOKFM", "TOK FM");

                LogManager.GetCurrentClassLogger().Info("IP: " + HttpContext.Request.UserHostAddress);
                return this.Content(xmlString, "application/rss+xml");
            }
            catch
            {
                throw new Exception("Return view rss error");
            }
        }


        public ActionResult Program()
        {
            Rss rssDataXML = new Rss();
            List<ProgramView> programs = new List<ProgramView>();

            rssDataXML.GetFromXML(path);
            programs.Add(rssDataXML.Channel.Items);
            LogManager.GetCurrentClassLogger().Info("IP: " + HttpContext.Request.UserHostAddress);

            return View(programs);
        }

        public ActionResult ProgramId(string id)
        {
            Rss rssDataXML = new Rss();
            rssDataXML.GetFromXML(path);

            List<Item> data = rssDataXML.Channel.Items.Where(p => p.Image2.Href.Contains(id)).ToList();
            ViewBag.ImageUrl = data.FirstOrDefault().Image2.Href;
            ViewBag.RSSUrl = id;

            LogManager.GetCurrentClassLogger().Info("IP: " + HttpContext.Request.UserHostAddress);

            return View("ProgramId", data);
        }

        public ActionResult AudioPlayer(string id)
        {
            Rss rssDataXML = new Rss();
            rssDataXML.GetFromXML(path);

            return PartialView("_AudioPlayer", rssDataXML.Channel.Items.Where(p => p.Guid.Text == id).FirstOrDefault());
        }

        public ActionResult Update()
        {
            Rss rssDataXML = new Rss();
            TOKFMWeb.Helpers.Update.XML();
            rssDataXML.GetFromXML(path);
            LogManager.GetCurrentClassLogger().Info("IP: " + HttpContext.Request.UserHostAddress);

            return View("Index", rssDataXML);

        }

        public FileResult OmplDownload()
        {
            string ret = "";
            Rss rssDataXML = new Rss();
            rssDataXML.GetFromXML(path);

            List<opmlOutline> opmlOutline = new List<Models.opmlOutline>();

            List<string> uniqueProgramsId = rssDataXML.Channel.Items.Select(p => p.Image2.Href).Distinct().ToList();

            if (uniqueProgramsId.Count > 0)
            {
                opml opmlFile = new opml(new opmlHead("PodcastAddict registration feeds", DateTime.Now.ToString(), DateTime.Now.ToString()),
                                       new opmlOutline[uniqueProgramsId.Count], 1);
                int i = 0;

                foreach (string item in uniqueProgramsId)
                {
                    opmlFile.body[i] = new opmlOutline("TOK FM", "rss", "http://tokfm.somee.com/Home/Rss/" +
                                            item.Substring(item.IndexOf("?") + 1), "http://www.tokfm.pl");
                    i++;
                }

                XmlSerializer sr = new XmlSerializer(typeof(opml));
                StringWriter tw = new StringWriter();
                sr.Serialize(tw, opmlFile);
                ret = tw.ToString();
                tw.Flush();
                tw.Close(); 
            }
            LogManager.GetCurrentClassLogger().Info("IP: " + HttpContext.Request.UserHostAddress);

            return File(Encoding.UTF8.GetBytes(ret), "application/xml", "OPML.xml");
        }
    }
}

