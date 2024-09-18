var jewels = new List<Jewel>();
var bag = new List<Jewel>();

// Get user data
Console.WriteLine("Enter the volume of the bag: ");
var bagVolume = Convert.ToInt32(Console.ReadLine());
var volumeLeft = bagVolume;

Console.WriteLine("Enter the number of items: ");
var numberOfItems = Convert.ToInt32(Console.ReadLine());

// Generate random jewels
for (int i = 0; i < numberOfItems; i++)
{
    var random = new Random();
    // Random volume and price based on user input
    var itemVolume = random.Next(1, bagVolume);
    var itemPrice = random.Next(1, 100);

    jewels.Add(new Jewel { volume = itemVolume, price = itemPrice });
}

// Fill the bag
jewels.Sort((a, b) => b.price - a.price);

foreach (var jewel in jewels)
{
    if (volumeLeft - jewel.volume >= 0)
    {
        bag.Add(jewel);
        volumeLeft -= jewel.volume;
    }
}

// Print the result

Console.WriteLine("Items in the bag:");
foreach (var jewel in bag)
{
    Console.WriteLine(jewel);
}
Console.WriteLine($"Total price: {bag.Sum(j => j.price)}");
Console.WriteLine($"Total volume: {bag.Sum(j => j.volume)}, Volume left: {volumeLeft}");