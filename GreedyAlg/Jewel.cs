public class Jewel
{
    public int volume { get; set; }
    public int price { get; set; }

    public override string ToString()
    {
        return $"Volume: {volume}, Price: {price}";
    }
}