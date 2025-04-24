using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAPI.Models
{
   
    
         public class CoordinatesModel
        {
            public int Id { get; set; }
            public int RouteId { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }

        }
    
}