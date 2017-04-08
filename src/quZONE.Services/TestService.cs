using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quZONE.Data.Interfaces;
using quZONE.Domain;
using quZONE.Domain.Entities;

namespace quZONE.Services
{
    public class TestService : ITestService
    {

        //private readonly IRepository<UserProfile> _userProfileRepoistory;

        ////public TestService() { }

        //public TestService(IRepository<UserProfile> userProfileRepoistory)
        //{
        //    _userProfileRepoistory = userProfileRepoistory;
        //}


        public string GetQuote()
        {
            return "6.9999";
        }
    }
}
