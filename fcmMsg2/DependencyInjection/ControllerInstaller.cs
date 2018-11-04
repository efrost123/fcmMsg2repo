using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Registration.Lifestyle;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace fcmMsg2.DependencyInjection
{
    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                                .BasedOn<IController>()
                                .If(t => t.Name.EndsWith("Controller"))
                                .LifestylePerWebRequest());
        }
    }
}