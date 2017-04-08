using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Ninject;
using Ninject.Syntax;
using quZONE.Data.Interfaces;
using quZONE.Data.Repositories;

namespace quZONE.Api.Infrastructure
{
    public class NinjectResolver : NinjectDependencyScope, IDependencyResolver
    {
        IKernel kernel;

      public NinjectResolver(IKernel kernel) : base(kernel)
      {
         this.kernel = kernel;
      }

      public IDependencyScope BeginScope()
      {
         return new NinjectDependencyScope(kernel.BeginBlock());
      }

        //private void AddBindings()
        //{
        //    // put bindings here
        //    //kernel.Bind<IBaseRepository<Roles>().To<BasedRepository<Roles>();
            

        //    //kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
        //    //kernel.Bind<IUnitOfWork>().To<UnitOfWork>();

        //    //kernel.Bind<IDbContext>().To<CommunityMarketContext>();
            
            
        //    //kernel.Bind<ICustomerRepository>().To<CustomerRepository>();


        //}


    }

    public class NinjectDependencyScope : IDependencyScope
    {
        IResolutionRoot resolver;

        public NinjectDependencyScope(IResolutionRoot resolver)
        {
            this.resolver = resolver;
        }

        public object GetService(Type serviceType)
        {
            if (resolver == null)
                throw new ObjectDisposedException("this", "This scope has been disposed");

            return resolver.TryGet(serviceType);
        }

        public System.Collections.Generic.IEnumerable<object> GetServices(Type serviceType)
        {
            if (resolver == null)
                throw new ObjectDisposedException("this", "This scope has been disposed");

            return resolver.GetAll(serviceType);
        }

        public void Dispose()
        {
            IDisposable disposable = resolver as IDisposable;
            if (disposable != null)
                disposable.Dispose();

            resolver = null;
        }
    }
}