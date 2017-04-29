using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TOKFMWeb.Helpers;

namespace TOKFMWeb.Models
{
    public class ProgramView
    {
        public string Id { get; set; }
        public string Guid { get; set; }
        public string Link { get; set; }
        public List<Item> Feed { get; set; }
    }
}