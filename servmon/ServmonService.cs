
using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.Runtime;

namespace servmon
{
    class ServmonService : ServiceControl
    {
        private readonly ILog logger;
        private IScheduler scheduler;
        private QuartzServer server;
        public ServmonService(HostSettings hostsetting)
        {
            try
            {
                logger = LogManager.GetLogger(GetType());
                Initialize();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        #region Initial Server
        public virtual void Initialize()
        {
            try
            {
                #region initial Quartz Server.
                server = QuartzServerFactory.CreateServer();
                server.Initialize();
                scheduler = server.GetScheduler();
                #endregion

            }
            catch (Exception e)
            {
                logger.Error("Server initialization failure:" + e.Message, e);
                throw;
            }
        }
        #endregion

        public bool Start(HostControl hostControl)
        {
            try
            {
                scheduler.Start();
            }
            catch (Exception ex)
            {
                logger.Fatal(string.Format("Service start failure: {0}", ex.Message), ex);
                throw (ex);
            }
            logger.Info("Service started successfully");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            try
            {
                scheduler.Shutdown();
                scheduler = null;
            }
            catch (Exception ex)
            {
                logger.Fatal(string.Format("Scheduler stop failure: {0}", ex.Message), ex);
                throw (ex);
            }
            logger.Info("Service stop successfully");
            return true;
        }
    }
}
