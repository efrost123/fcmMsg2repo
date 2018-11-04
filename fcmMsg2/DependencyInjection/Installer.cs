using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using fcmMsg2.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fcmMsg2.DependencyInjection
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IFcm>().ImplementedBy<FCM>().LifeStyle.PerWebRequest);
        }
    }
}