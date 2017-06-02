using FluentScheduler;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TOKFMWeb.Helpers
{
    public class AutomaticRefreshXML : Registry
    {
        public AutomaticRefreshXML()
        {
            Schedule<JobRefreshXML>().ToRunNow().AndEvery(10).Minutes();
        }
    }
}