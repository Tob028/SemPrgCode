using System;
using System.Collections.Generic;
using System.Diagnostics;

var cities = new Dictionary<string, (float lat, float lng)>();

using (var reader = new StreamReader("./worldcities.csv"))
{
    reader.ReadLine();

    while(!reader.EndOfStream)
    {
        var line = reader.ReadLine();
        var values = line.Split(',');

        var city = values[1].Trim('"');
        var adminName = values[7].Trim('"');
        var lat = float.Parse(values[2].Trim('"'));
        var lng = float.Parse(values[3].Trim('"'));

        var key = $"{city}, {adminName}";
        cities[city] = (lat, lng);
    }
}

var stopwatch = new Stopwatch();
stopwatch.Start();

float CalculateDistance(string city1, string city2)
{
    var lat1 = cities[city1].lat;
    var lng1 = cities[city1].lng;
    var lat2 = cities[city2].lat;
    var lng2 = cities[city2].lng;

    const float radius = 6371.0f; // km

    var dLat = DegToRad(lat2 - lat1);
    var dLng = DegToRad(lng2 - lng1);

    var a = MathF.Sin(dLat / 2) * MathF.Sin(dLat / 2) +
            MathF.Cos(DegToRad(lat1)) * MathF.Cos(DegToRad(lat2)) *
            MathF.Sin(dLng / 2) * MathF.Sin(dLng / 2);
    
    var d = 2 * MathF.Atan2(MathF.Sqrt(a), MathF.Sqrt(1 - a));

    return radius * d;
}

float DegToRad(float deg)
{
    return deg * MathF.PI / 180.0f;
}

// Prims MST
Dictionary<(string city1, string city2), float> MapTree()
{
    var unvisited = new HashSet<string>(cities.Keys);
    var edges = new Dictionary<(string city1, string city2), float>();
    var queue = new SortedSet<(float distance, string city1, string city2)>();
    
    var start = unvisited.First();
    unvisited.Remove(start);

    Console.WriteLine($"Start: {start}");

    foreach (var city in cities.Keys)
    {
        var distance = CalculateDistance(start, city);
        queue.Add((distance, start, city));
    }



    while (stopwatch.Elapsed.Seconds < 59 && queue.Count > 0)
    {
        var edge = queue.First();
        queue.Remove(edge);

        if (string.IsNullOrWhiteSpace(edge.city1) || string.IsNullOrWhiteSpace(edge.city2))
        {
            continue;
        }

        if (unvisited.Contains(edge.city2))
        {
            edges[(edge.city1, edge.city2)] = edge.distance;
            Console.WriteLine($"{edge.city1.PadRight(20)} -> {edge.city2.PadRight(20)} : {edge.distance:F0} km");
            unvisited.Remove(edge.city2);

            foreach (var city in unvisited)
            {
                var distance = CalculateDistance(edge.city2, city);
                queue.Add((distance, edge.city2, city));
            }
        }
    }

    stopwatch.Stop();
    return edges;
}

var edges = MapTree();

Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");
Console.WriteLine($"Total distance: {edges.Values.Sum()}");