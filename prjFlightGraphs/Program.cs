using System.Diagnostics;

namespace prjFlightGraphs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var network = new FlightNetwork();

            //add airports (vertices
            var jhb = network.addAirport("JHB (Joburg)");
            var cpt = network.addAirport("CPT (Cape Town)");
            var dur = network.addAirport("DUR (Durban)");
            var plz = network.addAirport("PLZ (Port Elizabeth)");
            var bfn = network.addAirport("BFN (Bloemfontein)");
            var grg = network.addAirport("GRG (George)");

            //add flight routes (weighted, directed edges
            //note: direct jhb to cpt is expensive 
            jhb.addRoutes(cpt, 2200); //direct ubt costly (2200)
            jhb.addRoutes(dur, 800); //jhb to dur cheap - 800
            dur.addRoutes(plz, 700); //dur to plz cheap - 700
            plz.addRoutes(cpt, 650); //plz to cpt cheap - 650
            jhb.addRoutes(bfn, 750); //cpt to bfn - alternate route
            bfn.addRoutes(cpt, 900); //bfn to cpt - 900
            bfn.addRoutes(grg, 1200); //bfn to grg - 1200

            Console.WriteLine("Finding flight routes from Joburg to Cape Town ...");
            Console.WriteLine("========================================================================\n");

            //Find the shortest path (fewest steps) with BFS
            Console.WriteLine("--- Running Breadth First Search (Finds route with fewest stops) ---");

            var shortestPath = network.findShortestPathBFS(jhb, cpt);
            if (shortestPath != null)
            {
                double cost = calculatePathCost(shortestPath);
                Console.WriteLine($"Shortest path found (Cost: R{cost}): ");
                Console.WriteLine(string.Join(" -> ", shortestPath.Select(p => p.name)));
            }
            else
            {
                Console.WriteLine("No route found.");
            }
            Console.WriteLine("\n========================================================================\n");
        }


        //helper function 
        public static double calculatePathCost(List<Airport> path)
        {
            double totalCost = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                Airport current = path[i];
                Airport next = path[i + 1];
                totalCost += current.routes[next];
            }

            return totalCost;
        }
    }
}
