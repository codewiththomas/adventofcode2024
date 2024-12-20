﻿using System.Linq;

string[] input = File.ReadAllLines("input.txt");


//static bool IsReportSafe(string report)
//{
//    int[] levels = report.Split().Select(int.Parse).ToArray();
//    return IsSequenceSafe(levels);
//}

//static bool IsSequenceSafe(int[] levels)
//{
//    bool? increasing = null;

//    for (int i = 0; i < levels.Length - 1; i++)
//    {
//        int diff = levels[i + 1] - levels[i];

//        if (diff == 0 || Math.Abs(diff) > 3) return false; // Quick fail if difference is 0 or >3

//        if (increasing == null)
//        {
//            increasing = diff > 0;
//        }
//        else if ((increasing.Value && diff < 0) || (!increasing.Value && diff > 0))
//        {
//            return false;
//        }
//    }

//    return true;
//}

//static bool CanBeMadeSafe(string report)
//{
//    int[] levels = report.Split().Select(int.Parse).ToArray();

//    for (int i = 0; i < levels.Length; i++)
//    {
//        // Create a subsequence without the current level
//        int[] modifiedLevels = levels.Take(i).Concat(levels.Skip(i + 1)).ToArray();

//        if (IsSequenceSafe(modifiedLevels))
//        {
//            return true;
//        }
//    }

//    return false;
//}


static bool isReportSave(int[] levels, int skipAbleCount = 0)
{
    Console.WriteLine("  Use list: " + string.Join(" ", levels));

    bool? globalDirection = null;
    int? failIndex = null;

    for (int i = 1; i < levels.Length; i++)
    {
        //check direction
        bool localDirection = levels[i - 1] < levels[i];
        globalDirection ??= localDirection;

        if (globalDirection != localDirection)
        {
            Console.WriteLine("  -> Unsafe due direction change");
            failIndex = i;
            break;
        }

        //check distance
        int distance = Math.Abs(levels[i - 1] - levels[i]);

        if (distance == 0 || distance > 3)
        {
            Console.WriteLine("  -> Unsafe due distance");
            failIndex = i;
            break;
        }
    }

    if (failIndex.HasValue)
    {
        if (skipAbleCount > 0)
        {
            var modifiedReport = levels.Take(failIndex.Value).Concat(levels.Skip(failIndex.Value + 1)).ToArray();
            Console.WriteLine("  -> Skip one element");
            return isReportSave(levels.Take(failIndex.Value).Concat(levels.Skip(failIndex.Value + 1)).ToArray(), skipAbleCount - 1);
        }
        else
        {
            return false;
        }
    }
    return true;


}

int safeReportsLevelOneCount = 0;
int safeReportsLevelTwoCount = 0;

foreach (string report in input)
{
    Console.WriteLine($"======== SAMPLE {report} ==========");

    var reportValues = report
        .Split(' ')
        .Select(x => int.Parse(x))
        .ToArray();

    //if (isSave(values))
    //{
    //    safeReportsLevelOneCount++;
    //}

    if (isReportSave(reportValues, 1))
    {
        safeReportsLevelTwoCount++;
    }
}

//Console.WriteLine("Safe reports count (level one): " + safeReportsLevelOneCount.ToString());
Console.WriteLine("Safe reports count (level two): " + safeReportsLevelTwoCount.ToString());

//int safeReports = lines.Count(report => IsReportSafe(report) || CanBeMadeSafe(report));

//Console.WriteLine($"Number of safe reports: {safeReports}");