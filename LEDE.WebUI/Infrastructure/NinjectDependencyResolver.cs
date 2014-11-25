using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Ninject;
using System.Web.Mvc;
using LEDE.Domain.Abstract;
using LEDE.Domain.Concrete;
using LEDE.Domain.Entities;

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
            /*
            //////////////////////////
            //Summary Repository Mock 
            
            ////Summary View        
            Mock<ISummaryRepository> mockSummary = new Mock<ISummaryRepository>();

            //mocking getcohorttotals
            SeminarSummary summary1 = new SeminarSummary();
            summary1.MaxTotal = 26;

            List<StudentTotal> totals1 = new List<StudentTotal>(); summary1.TotalsList = totals1; 
            User student1 = new User() { Id = 1, UserName = "student1", FirstName="Test", LastName="Student1" };
            StudentTotal student1Totals = new StudentTotal(){CTotal=13, PTotal=12, STotal=1, User = student1};
            totals1.Add(student1Totals);

            User student2 = new User() { Id = 2, UserName = "student2", FirstName = "Test", LastName = "Student2" };
            StudentTotal student2Totals = new StudentTotal() { CTotal = 5, PTotal = 5, STotal = 2, User = student2 };
            mockSummary.Setup(m => m.getCohortTotals(1)).Returns(summary1);
            totals1.Add(student2Totals);

            SeminarSummary summary2 = new SeminarSummary();
            summary2.MaxTotal = 13; 
            List<StudentTotal> totals2 = new List<StudentTotal>(); summary2.TotalsList = totals2;
            User student3 = new User() { Id = 3, UserName = "student3", FirstName = "Test", LastName = "Student3" };
            StudentTotal student3Totals = new StudentTotal() { CTotal = 2, PTotal = 5, STotal = 6, User = student3 };
            totals2.Add(student3Totals);

            User student4 = new User() { Id = 4, UserName = "student4", FirstName = "Test", LastName = "Student4" };
            StudentTotal student4Totals = new StudentTotal() { CTotal = 5, PTotal = 5, STotal = 2, User = student4 };
            mockSummary.Setup(m => m.getCohortTotals(2)).Returns(summary2);
            totals2.Add(student4Totals); 

            //mocking getsummary
            List<ProgramCohort> cohorts = new List<ProgramCohort>();
            Program LEDE = new Program() {
                ProgramID = 1,
                ProgramTitle = "LEDE1",
                ProgramType = "LEDE",
            };
            Program ECSEL = new Program() {
                ProgramID = 2,
                ProgramTitle = "ECSEL1",
                ProgramType = "ECSEL",
            };
            ProgramCohort LEDECohort = new ProgramCohort {
                ProgramCohortID= 1,
                ProgramID = 1,
                Program = LEDE,
                AcademicYear = "2014",
                Status = "Current"
            };
            ProgramCohort ECSELCohort = new ProgramCohort {
                ProgramCohortID= 2,
                ProgramID = 2,
                Program = ECSEL,
                AcademicYear = "2014",
                Status = "Current"
            };
            cohorts.Add(LEDECohort); cohorts.Add(ECSELCohort);

            mockSummary.Setup(m => m.getCohorts()).Returns(cohorts); 
            */ 
            /*
            ////Student View
            //mocking getstudenttotals
            List<CoreTotal> ratingsList1 = new List<CoreTotal>();
            CoreTopic coreTopic1 = new CoreTopic()
                {
                    CoreTopicID = 1,
                    CoreTopicDesc = "CoreTopic1",
                    CoreTopicNum = (decimal)1.1,
                    ModifyDate = DateTime.Now,
                    SeminarID = 1,
                    Status = "Current"
                };
            CoreTopic coreTopic2 = new CoreTopic()
            {
                CoreTopicID = 2,
                CoreTopicDesc = "CoreTopic2",
                CoreTopicNum = (decimal)1.2,
                ModifyDate = DateTime.Now,
                SeminarID = 1,
                Status = "Current"
            };
            CoreTotal coreTotal1 = new CoreTotal
            {
                CoreTopic = coreTopic1,
                CTotal = 12,
                STotal = 3,
                PTotal = 5
            };

            ratingsList1.Add(coreTotal1);
            StudentSummary studentSummary1 = new StudentSummary(){
                RatingsList = ratingsList1,
                MaxTotal = 20,
                User = student1
            };

            PercentageCalculator calc = new PercentageCalculator();
            calc.CalculateStudentPercentages(studentSummary1); 

            mockSummary.Setup(m => m.getStudentTotals(It.IsAny<int>())).Returns(studentSummary1);            

            kernel.Bind<ISummaryRepository>().ToConstant(mockSummary.Object); 
            */

            kernel.Bind<ISummaryRepository>().To<SummaryRepository>();
            kernel.Bind<ICandidateRepository>().To<CandidateRepository>(); 
            kernel.Bind<IRatingRepository>().To<RatingRepository>();
            kernel.Bind<IEnrollmentRepository>().To<EnrollmentRepository>(); 
            kernel.Bind<IUserRepository>().To<UserRepository>();

            kernel.Bind<IPercentageCalculator>().To<PercentageCalculator>(); 
        }

    }
}