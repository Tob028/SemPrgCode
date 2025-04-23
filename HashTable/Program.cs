var table = new HashTable(10);

table.Insert(100, "Hello");
table.Insert(101, "World");
table.Insert(200, "This one should break it");
table.Insert(202, "And this one should be removed");

Console.WriteLine(table.Search(100));
Console.WriteLine(table.Search(101));

Console.WriteLine(table.Search(202));
table.Delete(202);
Console.WriteLine(table.Search(202));


public class HashTable
{
    private int size;
    private List<KeyValuePair<int, string>>[] hashTable;

    public HashTable(int size)
    {
        this.size = size;
        hashTable = new List<KeyValuePair<int, string>>[size];

        for (int i = 0; i < size; i++)
        {
            hashTable[i] = new List<KeyValuePair<int, string>>();
        }
    }

    public void Insert(int key, string value)
    {
        var index = key % size;
        hashTable[index].Add(new KeyValuePair<int, string>(key, value));
    }

    public string Search(int key)
    {
        var index = key % size;
        foreach (var entry in hashTable[index])
        {
            if (entry.Key == key)
            {
                return entry.Value;
            }
        }
        return null;
    }

    public void Delete(int key)
    {
        var index = key % size;
        for (int i = 0; i < hashTable[index].Count; i++)
        {
            if (hashTable[index][i].Key == key)
            {
                hashTable[index].Remove(hashTable[index][i]);
            }
        }
    }
}

