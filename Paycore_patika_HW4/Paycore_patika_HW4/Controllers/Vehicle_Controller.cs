using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Paycore_patika_HW3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Vehicle_Controller : ControllerBase
    {


        private readonly ISession session;
        private ITransaction transaction;
        public Vehicle_Controller(ISession session)
        {
            this.session = session;
        }


        [HttpGet("All Data")]
        public List<Vehicle> Get()
        {
            var response = session.Query<Vehicle>().ToList();
            return response;
        }



    }
}