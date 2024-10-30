var cityGraph = new Dictionary<(string, string), int>();

// Load data

using(var reader = new StreamReader("./data_reduced.csv"))
{
    while(!reader.EndOfStream)
    {
        var line = reader.ReadLine();
        var values = line.Split(',');

        cityGraph.Add((values[0], values[1]), int.Parse(values[2]));
        cityGraph.Add((values[1], values[0]), int.Parse(values[2]));
    }
}



// Get input

Console.WriteLine("Startovní město:");
var start = Console.ReadLine();
if (!cityGraph.Keys.Any(c => c.Item1 == start) || !cityGraph.Keys.Any(c => c.Item2 == start))
{
    Console.WriteLine("Start neexistuje");
    return;
}

Console.WriteLine("Cílové město:");
var end = Console.ReadLine();
if (!cityGraph.Keys.Any(c => c.Item1 == end) || !cityGraph.Keys.Any(c => c.Item2 == end))
{
    Console.WriteLine("Cíl neexistuje");
    return;
}

// Dijkstra

var distances = new Dictionary<string, int>(); // shortest distance to each city
var previous = new Dictionary<string, string>(); // previous visited city - to reconstruct the path
var unvisited = new List<string>(); // cities not yet visited

foreach (var city in cityGraph)
{
    distances[city.Key.Item1] = int.MaxValue;
    unvisited.Add(city.Key.Item1);
}
distances[start] = 0;
unvisited.Remove(start);

while (unvisited.Count > 0)
{
    string currentCity = unvisited.OrderBy(c => distances[c]).First();

    foreach (var neighbour in unvisited)
    {
        if (cityGraph.ContainsKey((currentCity, neighbour)))
        {
            var distance = distances[currentCity] + cityGraph[(currentCity, neighbour)];
            if (distance < distances[neighbour])
            {
                distances[neighbour] = distance;
                previous[neighbour] = currentCity;
            }
        }
    }

    unvisited.Remove(currentCity);
}

var distanceToEnd = distances[end];
Console.WriteLine($"Vzdálenost {distanceToEnd}");

var path = new List<string>();
var currentPathCity = end;

while (currentPathCity != start)
{
    path.Add(currentPathCity);
    currentPathCity = previous[currentPathCity];
}
path.Add(start);

path.Reverse();

Console.WriteLine($"Cesta: {string.Join(" -> ", path)}");
