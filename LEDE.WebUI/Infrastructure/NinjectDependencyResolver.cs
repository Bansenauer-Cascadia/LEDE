using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Ninject;
using System.Web.Mvc;
using LEDE.Domain.Abstract;
using LEDE.Domain.Concrete;
using LEDE.Domain.Entities;
using LEDE.Domain.Repositories;
namespace LEDE.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public void AddBindings()
        {           
            kernel.Bind<ISummaryRepository>().To<SummaryRepository>();
            kernel.Bind<ICandidateRepository>().To<CandidateRepository>(); 
            kernel.Bind<IRatingRepository>().To<RatingRepository>();
            kernel.Bind<IEnrollmentRepository>().To<EnrollmentRepository>(); 
            kernel.Bind<IUserRepository>().To<UserRepository>();

            kernel.Bind<IProgramRepository>().To<ProgramRepository>();
            kernel.Bind<ITaskVersionRepository>().To<TaskVersionRepository>();

            kernel.Bind<IPercentageCalculator>().To<PercentageCalculator>(); 
        }

    }
}