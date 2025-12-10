using System.Numerics;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.Win32.SafeHandles;

namespace AdventOfCode2025.Day2;

public class Day2
{
    private string _filePath = "/Users/a3emond/dev/AdventOfCode2025/AdventOfCode2025/AdventOfCode2025/Day2/input.txt";
    private string _input;
    private string[] _ranges; // item format "123-456"
    private List<long> invalidIds;
    
    public Day2()
    {
        _input = File.ReadAllText(_filePath);
        _ranges = _input.Split(",");
        invalidIds = new List<long>();
    }
    
    public BigInteger SumInvalidIds()
    {
        foreach (string range in _ranges)
        {
            var bounds = range.Split("-");
            Console.WriteLine($"Processing range: {bounds[0]} to {bounds[1]}");
            var start = long.Parse(bounds[0]);
            var end = long.Parse(bounds[1]);
            
            for (long id = start; id <= end; id++)
            {
                if (IsInvalid(id))
                {
                    invalidIds.Add(id);
                }
            }
        }

        return invalidIds.Sum();
    }

    private bool IsInvalid(long id)
    {
        //V1
        // invalid ids are define by being made of repeating digit sequence twice only like 123123 or 11
        /*
        string idStr = id.ToString();
        int len = idStr.Length;
        if (len < 2) return false;
        if (len % 2 != 0) return false;
        if (idStr.Substring(0, len / 2) == idStr.Substring(len / 2, len / 2))
        {
            return true;
        }
        return false;
        */

        //V2
        // invalid ids are define by being made of repeating digit sequence any number of times like 123123123 or 1111
        string idStr = id.ToString();
        int len = idStr.Length;
        // Check for all possible substring lengths
        for (int subLen = 1; subLen <= len / 2; subLen++)
        {
            if (len % subLen != 0) continue; // Must divide evenly

            // get the first substring
            string subStr = idStr.Substring(0, subLen);
            // check if repeating this substring forms the original string
            int repeatCount = len / subLen;
            string constructedStr = string.Concat(Enumerable.Repeat(subStr, repeatCount));
            if (constructedStr == idStr)
            {
                return true;
            }
        }
        return false;
    }
}