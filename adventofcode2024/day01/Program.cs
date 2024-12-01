//read input.txt file
string[] lines = File.ReadAllLines("input.txt");

var firstlist = new List<int>();
var secondlist = new List<int>();

foreach (string line in lines)
{
    var values = line.Split("   ");
    int a = int.Parse(values[0]);
    int b = int.Parse(values[1]);
    firstlist.Add(a);
    secondlist.Add(b);
}

// SOLUTION FIRST PART

var orderedFirstList = firstlist.OrderBy(x => x).ToArray();
var orderedSecondList = secondlist.OrderBy(x => x).ToArray();

int totalDistance = 0;

for (int i = 0; i < orderedFirstList.Length; i++)
{
    int distance = orderedSecondList[i] - orderedFirstList[i];
    totalDistance += Math.Abs(distance);
}

Console.WriteLine("Total distance: " + totalDistance.ToString());

// SOLUTION SECOND PART
int totalSimilarityScore = 0;

for (int i = 0; i < orderedFirstList.Length; i++)
{
    int number = orderedFirstList[i];
    int occurences = orderedSecondList.Count(x => x == number);
    int similarityScore = occurences * number;
    totalSimilarityScore += similarityScore;
}

Console.WriteLine("Total similarity score: " + totalSimilarityScore.ToString());


