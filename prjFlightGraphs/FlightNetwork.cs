using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjFlightGraphs
{
    public class FlightNetwork
    {
        public List<Airport> airports { get; } = new List<Airport>();

        public Airport addAirport(string name)
        {
            var airport = new Airport(name);
            airports.Add(airport);
            return airport;
        }

        //ALGORITHM 1: BFS A SHORTEST PATH (fewest stops)
        public List<Airport> findShortestPathBFS(Airport start, Airport end)
        {
            var queue = new Queue<Airport>();
            var visited = new HashSet<Airport> { start };
            var parentMap = new Dictionary<Airport, Airport>();

            queue.Enqueue(start); //queue of all airports begin form starting airport

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (current == end) //if it is the destination airport
                {
                    //found the end, so reconstrcut the path
                    var path = new List<Airport>();
                    var step = end;

                    while (step != null)
                    {
                        path.Add(step);
                        parentMap.TryGetValue(step, out step);
                    }

                    path.Reverse();
                    return path;
                }

                foreach (var route in current.routes)
                {
                    var neighbor = route.Key;
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        parentMap[neighbor] = current;
                        queue.Enqueue(neighbor); //move the queue to the next airport in the path (bcuz it dequeues at the start)
                    }
                }
            }

            return null; //no ptah was fohund
        }


        //ALGORITHM 2: DFS for any path
        public List<Airport> findAnyPathDFS(Airport start, Airport end)
        {
            var visited =  new HashSet<Airport>();
            return findPathRecursive(start, end, visited);
        }

        public List<Airport> findPathRecursive(Airport current, Airport end, HashSet<Airport> visited)
        {
            visited.Add(current);

            if (current == end)
            {
                return new List<Airport> { end };
            }

            foreach (var route in current.routes)
            {
                var neighbor = route.Key;
                if (!visited.Contains(neighbor))
                {
                    List<Airport> pathFromNeightbor = findPathRecursive(neighbor, end, visited);
                    if (pathFromNeightbor != null)
                    {
                        pathFromNeightbor.Insert(0, current);
                        return pathFromNeightbor;
                    }
                }
            }
            return null; //no ptah was fohund
        }
    }
}
