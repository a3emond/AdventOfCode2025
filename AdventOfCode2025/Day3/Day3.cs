namespace AdventOfCode2025;

public class Day3
{
    private string _path = "../../../Day3/Input.txt";
    private readonly string[] _powerBanks;
    private long _totalJoltage = 0;
    
    public Day3()
    {
        _powerBanks = System.IO.File.ReadAllLines(_path);
        
        Console.WriteLine($"Total Joltage from Power Banks: {FindTotalJoltage(_powerBanks)}");
    }

    private long FindTotalJoltage(string[] input)
    {
        foreach (var line in input)
        {
            _totalJoltage += FindSuperMaxJoltage(line);
        }       
        return _totalJoltage;
    }
    
    // helper methods
    private int FindMaxJoltage(string bank)
    {
        var batteryCount = bank.Length;
        var maxJoltage = 0;
        for (int i = 0; i < batteryCount; i++)
        {
            for (int j = i + 1; j < batteryCount; j++)
            {
                string joltage = bank[i].ToString() + bank[j].ToString();
                int joltageValue = int.Parse(joltage);
                
                if (joltageValue > maxJoltage)
                    maxJoltage = joltageValue;
            }
        }
        return maxJoltage;
    }
    
    private long FindSuperMaxJoltage(string bank) // 12 digit joltage
    {
        var batteryCount = bank.Length;
        const int k = 12;
        if (batteryCount <= k)
            return long.Parse(bank);

        var stack = new List<char>(batteryCount);
        int toRemove = batteryCount - k;

        for (int i = 0; i < batteryCount; i++)
        {
            char c = bank[i];// current character
            while (stack.Count > 0 && stack[stack.Count - 1] < c && toRemove > 0)
            {
                stack.RemoveAt(stack.Count - 1);
                toRemove--;
            }
            stack.Add(c);
        }

        if (stack.Count > k)
        {
            var resultChars = new char[k];
            for (int i = 0; i < k; i++) resultChars[i] = stack[i];
            return long.Parse(new string(resultChars));
        }

        return long.Parse(new string(stack.ToArray()));
    }
}