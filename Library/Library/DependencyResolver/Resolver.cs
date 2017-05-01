using Library.DAL;
using Library.DAL.Common;
using Library.Service;
using Library.Service.Common;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyResolver
{
    public class Resolver : NinjectModule
    {
        public override void Load()
        { 
            Bind<IGeneric>().To<Generic>();
            Bind<ILibraryContext>().To<LibraryContext>();
            Bind<IKorisnikService>().To<KorisnikService>();
            Bind<IKnjiznicaService>().To<KnjiznicaService>();
            Bind<IKnjigaService>().To<KnjigaService>();
            Bind<IPosudenaKnjigaService>().To<PosudenaKnjigaService>();
            Bind<IOdjelService>().To<OdjelService>();
            //throw new NotImplementedException();
        }
    }
}
