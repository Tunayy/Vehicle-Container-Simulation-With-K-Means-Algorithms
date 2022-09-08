using FluentNHibernate.Testing.Values;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.Mapping;
using System.Collections.Generic;
using System.Linq;
using System;


namespace Paycore_patika_HW3.Controllers
{
    
    
    public class Class
    {
        public void Deneme(List list)
        {

            List<double> kartezyanX = new List<double>();
            List<double> kartezyanY = new List<double>();
        }
    }



    [ApiController]
    [Route("api/[controller]")]
    public class Container_Controller : ControllerBase
    {
        
        private readonly ISession session;
        private ITransaction transaction;
        public Container_Controller(ISession session)
        {
            this.session = session;
        }

        [HttpGet]
        public List<Container> Get()
        {
            var response = session.Query<Container>().ToList();
            return response;
        }

        [HttpPut]
        public ActionResult<Container> Put(Container request)
        {
            int i =0;
            int k= 0;
            
            List<double> listX = new List<double>();
            List<double> listY = new List<double>();

            List<double> listX_For_A = new List<double>();
            List<double> listY_For_A = new List<double>();

            List<double> listX_For_B = new List<double>();
            List<double> listY_For_B = new List<double>();

            List<double> listX_For_C = new List<double>();
            List<double> listY_For_C = new List<double>();

            List<long> id = new List<long>();
            List<long> Real_id = new List<long>();
            List<long> Parametr_id = new List<long>();

            var response = session.Query<Container>().ToList();
            var Cont = new Container();
            //Cont.Latitude = response[0].Latitude;
            while(i < response.Count)
            {
                listX.Add(response[i].Latitude);
                listY.Add(response[i].Longitude);
                Parametr_id.Add(response[i].Id);
                i++;
            }

            while  (k < response.Count)
            {
                if( k!=3 && k!=7 && k!=12)
                {
                    double lengthA= Math.Sqrt(Math.Pow((listY[3] - listY[k]), 2) + Math.Pow((listX[3] - listX[k]), 2));
                    double lengthB = Math.Sqrt(Math.Pow((listY[7] - listY[k]), 2) + Math.Pow((listX[7] - listX[k]), 2));
                    double lengthC = Math.Sqrt(Math.Pow((listY[12] - listY[k]), 2) + Math.Pow((listX[12] - listX[k]), 2));

                    if( lengthA < lengthB && lengthA<lengthC)
                    {
                        id.Add(response[3].VehicleId);
                    }

                    else if (lengthB < lengthA && lengthB < lengthC)
                    {
                        id.Add(response[7].VehicleId);
                    }

                    else if (lengthC < lengthA && lengthC < lengthB)
                    {
                        id.Add(response[12].VehicleId);
                    }
                }

                k++;
            }

            int j = 0;
            int m = 0;
            while (j <response.Count)
            {
                if (j != 3 && j != 7 && j != 12)
                {
                    if (id[m] == 1)
                    {
                        listX_For_A.Add(listX[j]);
                        listY_For_A.Add(listY[j]);
                    }

                    else if (id[m] == 2)
                    {
                        listX_For_B.Add(listX[j]);
                        listY_For_B.Add(listY[j]);
                    }

                    else if (id[m] == 3)
                    {
                        listX_For_C.Add(listX[j]);
                        listY_For_C.Add(listY[j]);
                    }
                    m++;
                }
                j++;
            }


            double RealLengthX_A = 0;
            double RealLengthX_B = 0;
            double RealLengthX_C =0;

            if (listX_For_A.Count!=0)
            {
                RealLengthX_A = listX_For_A.Average();

            }
            if (listX_For_B.Count != 0)
            {
                RealLengthX_B = listX_For_B.Average();

            }
            if (listX_For_C.Count != 0)
            {
                RealLengthX_C = listX_For_C.Average();

            }


            double RealLengthY_A = 0;
            double RealLengthY_B = 0; 
            double RealLengthY_C = 0;

            if (listY_For_A.Count != 0)
            {
                RealLengthY_A = listY_For_A.Average();

            }
            if (listY_For_B.Count != 0)
            {
                RealLengthY_B = listY_For_B.Average();

            }
            if (listY_For_C.Count != 0)
            {
                RealLengthY_C = listY_For_C.Average();

            }



            k = 0;
            while (k < response.Count)
            {
                
                double RealLengthA = Math.Sqrt(Math.Pow((RealLengthY_A - listY[k]), 2) + Math.Pow((RealLengthX_A - listX[k]), 2));
                double RealLengthB = Math.Sqrt(Math.Pow((RealLengthY_B - listY[k]), 2) + Math.Pow((RealLengthX_B - listX[k]), 2));
                double RealLengthC = Math.Sqrt(Math.Pow((RealLengthY_C - listY[k]), 2) + Math.Pow((RealLengthX_C - listX[k]), 2));

                if (RealLengthA < RealLengthB && RealLengthA < RealLengthC)
                {
                    Real_id.Add(1);
                }

                else if (RealLengthB < RealLengthA && RealLengthB < RealLengthC)
                {
                    Real_id.Add(2);
                }

                else if (RealLengthC < RealLengthA && RealLengthC < RealLengthB)
                {
                    Real_id.Add(3);
                }


                k++;
            }


            k=0;
            while (k < response.Count)
            {
                using (var transaction = session.BeginTransaction())
                {
                    Container container = session.Query<Container>().Where(x => x.Id == Parametr_id[k]).FirstOrDefault();  
                    container.VehicleId = Real_id[k];
                    session.Update(container);
                    transaction.Commit();
                   
                }

                k++;
            }

            return Ok();
        }




    }



}