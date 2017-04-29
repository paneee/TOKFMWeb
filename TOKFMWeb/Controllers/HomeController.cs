﻿using MoreLinq;
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
        public HomeController()
        {
            path = HostingEnvironment.MapPath(@"~/XMLDataFile/RSS.xml");
        }

        public ActionResult Index()
        {
            Rss rssXML = new Rss();
            rssXML.GetFromXML(path);
            return View(rssXML);
        }

        public ActionResult Rss()
        {
            Rss rssDataXML = new Rss();
            rssDataXML.GetFromXML(path);
            rssDataXML.Atom = "http://www.w3.org/2005/Atom";
            rssDataXML.Itunes = "http://www.itunes.com/dtds/podcast-1.0.dtd";

            try
            {
                var sw = new System.IO.StringWriter();
                var serializer = new XmlSerializer(typeof(Rss));
                serializer.Serialize(sw, rssDataXML);

                string xmlString = sw.ToString();

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

            return View(programs);
        }

        public ActionResult ProgramId(string id)
        {
            Rss rssDataXML = new Rss();
            rssDataXML.GetFromXML(path);

            List<Item> data = rssDataXML.Channel.Items.Where(p => p.Image2.Href.Contains(id)).ToList();
            ViewBag.ImageUrl = data.FirstOrDefault().Image2.Href;

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
            return View("Index", rssDataXML);

        }
    }
}