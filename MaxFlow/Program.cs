using (var reader = new StreamReader("./input.txt"))
{
    int[] line;
    line = reader.ReadLine().Split(' ').Select(int.Parse).ToArray();
    var nodes = line[0];
    var edges = line[1];
    line = reader.ReadLine().Split(' ').Select(int.Parse).ToArray();
    var start = line[0];
    var end = line[1];

    var graph = new Dictionary<(int from, int to), (int flow, int capacity)>();
    for (var i = 0; i < nodes + 1; i++)
    {
        line = reader.ReadLine().Split(' ').Select(int.Parse).ToArray();
        graph.Add((line[0], line[1]), (0, line[2]));
    }

    Solve(graph, start, end);
}

void Solve(Dictionary<(int from, int to), (int flow, int capacity)> graph, int start, int end)
{
    var maxFlow = 0;

    // first fill graph to maximum with bfs
    var previous = new Dictionary<int, int>();
    var visited = new HashSet<int>();
    var queue = new Queue<int>();
    queue.Enqueue(start);

    while (queue.Count > 0)
    {
        var current = queue.Dequeue();
        visited.Add(current);

        if (current == end)
        {
            var maxFlowForPath = int.MaxValue;
            var node = end;
            var path = new List<(int from, int to)>();
            while (node != start)
            {
                var previousNode = previous[node];
                var edge = (previousNode, node);
                path.Add(edge);
                var flowAvailable = graph[edge].capacity - graph[edge].flow;
                maxFlowForPath = Math.Min(flowAvailable, maxFlowForPath);
                node = previousNode;
            }

            foreach (var edge in path)
            {
                var currentFlow = graph[edge].flow;
                var finalFlow  = currentFlow + maxFlowForPath;
                graph[edge] = (finalFlow, graph[edge].capacity);
            }
        }

        foreach (var edge in graph.Keys)
        {
            var (from, to) = edge;
            var (flow, capacity) = graph[edge];

            if (from == current && !visited.Contains(to) && flow < capacity)
            {
                queue.Enqueue(to);
                previous[to] = current;
            }
        }
    }

    foreach (var node in graph)
    {
        Console.WriteLine($"{node.Key.from} > {node.Key.to}: {node.Value.flow}/{node.Value.capacity}");
    }
    // add reverse edges
    // then iterate including reverse edges until no more paths are found
}