using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TOKFMWeb.Models;

namespace TOKFMWeb.Helpers
{
    public static class Extention
    {
        public static void Add(this List<ProgramView> programView, List<Item> items)
        {
            //  List<Item> 
            var uniqueItem = items.DistinctBy(p => p.Image2.Href).ToList();
            foreach (var item in uniqueItem)
            {
                programView.Add(new ProgramView
                {
                    Id = item.Image2.Href.Substring(item.Image2.Href.IndexOf("?") + 1),
                    Guid = item.Guid.Text,
                    Link = item.Image2.Href,
                    Feed = items.Where(p => p.Image2.Href == item.Image2.Href).ToList()
                });
            }
        }
    }
}