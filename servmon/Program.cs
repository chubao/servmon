using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace servmon
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(host =>
            {
                host.Service<ServmonService>(s =>
                {
                    s.ConstructUsing(name => new ServmonService(name));
                    s.WhenStarted((tc, hostControl) => tc.Start(hostControl));
                    s.WhenStopped((tc, hostControl) => tc.Stop(hostControl));
                });

                host.RunAsLocalSystem();
                host.StartAutomaticallyDelayed();
                host.SetDescription(Configuration.ServiceDescription);
                host.SetDisplayName(Configuration.ServiceDisplayName);
                host.SetServiceName(Configuration.ServiceName);
            });
        }
    }
}
