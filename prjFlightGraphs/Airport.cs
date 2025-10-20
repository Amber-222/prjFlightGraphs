using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjFlightGraphs
{
    public class Airport
    {
        public string name { get; }
        //key: destinatino airport; value: Cost of flight 
        public Dictionary<Airport, double> routes { get; }
        public Airport(string name)
        {
            this.name = name;
            routes = new Dictionary<Airport, double>();
        }

        //helper for a one way flight
        public void addRoutes(Airport destination, double cost)
        {
            routes[destination] = cost;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
