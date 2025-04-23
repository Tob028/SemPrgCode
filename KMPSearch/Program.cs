void main()
{
    Console.WriteLine(string.Join(", ", ComputeLPS("abab")));
}

int[] ComputeLPS(string pattern)
{
    var lpsTable = new int[pattern.Length];
    var scrapPattern = new String(pattern);
    
    for (int i = 0; i < pattern.Length; i++)
    {
        int j = 0;
        while (true)
        {
            if (scrapPattern[j] == pattern[j])
            {
                j++;
            }
            break;
        }

        lpsTable[i] = j;
        scrapPattern = scrapPattern.Substring(1);
    }
    
    lpsTable[0] = 0;
    return lpsTable;
}

List<int> KMPSearch(string text, string pattern)
{
    var appearances = new List<int>();
    var lpsTable = ComputeLPS(text);

    return appearances;
}

main();