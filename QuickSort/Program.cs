int[] quickSort(int[] input)
{
    if (input.Length < 2) return input;
    var pivot = 0; // pointer to pivot

    var left = new List<int>();
    var right = new List<int>();

    foreach (int i in input)
    {
        if (i < input[pivot])
        {
            left.Add(i);
            continue;
        }
        right.Add(i);
    }

    var leftRes = quickSort(left.ToArray());
    var rightRes = quickSort(right.ToArray());

    var result = leftRes.Concat(rightRes).ToArray();

    return result;
}

void swapValues(int[] arr, int a, int b)
{
    var buffer = arr[a];
    arr[a] = arr[b];
    arr[b] = buffer;
}

void main()
{
    Console.WriteLine(string.Join(", ", quickSort([7, 2, 5, 10, 1, 132, 23])));
}

main();