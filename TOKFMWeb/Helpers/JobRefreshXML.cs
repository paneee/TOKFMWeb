using FluentScheduler;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace TOKFMWeb.Helpers
{
    public class JobRefreshXML : IJob, IRegisteredObject
    {
        //private Rss rssDataWEB = new Rss();
        //private Rss rssDataXML = new Rss();

        private readonly object _lock = new object();

        private bool _shuttingDown;

        public JobRefreshXML()
        {
            // Register this job with the hosting environment.
            // Allows for a more graceful stop of the job, in the case of IIS shutting down.
            HostingEnvironment.RegisterObject(this);
        }

        public void Execute()
        {
            lock (_lock)
            {
                if (_shuttingDown)
                    return;

                Update.XML(); 
                LogManager.GetCurrentClassLogger().Info("Autoupdate");
            }
        }

        public void Stop(bool immediate)
        {
            // Locking here will wait for the lock in Execute to be released until this code can continue.
            lock (_lock)
            {
                _shuttingDown = true;
            }

            HostingEnvironment.UnregisterObject(this);
        }
    }
}