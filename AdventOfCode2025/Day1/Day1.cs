namespace AdventOfCode2025.Day1;

public static class Day1
{
    private static readonly string _filePath = "/Users/a3emond/dev/AdventOfCode2025/AdventOfCode2025/AdventOfCode2025/Day1/input.txt";
    private static readonly string[] _inputLines = File.ReadAllLines(_filePath);

    private static int dialPosition = 50;

    
    // Calculate how many times the dial stops at position 0
    public static (int, int) CalculateDialStopsAtZero() 
    {
        int stopsAtZero = 0;
        int clicksAtZero = 0;

        foreach (string line in _inputLines)
        {
            var (direction, value) = GetDialMovement(line);

            for (int i = 0; i < value; i++)
            {
                dialPosition += direction;

                // Wrap around the dial
                if (dialPosition < 0)
                {
                    dialPosition = 99;
                }
                else if (dialPosition > 99)
                {
                    dialPosition = 0;
                }
                
                if (dialPosition == 0)
                {
                    clicksAtZero++;
                }
            }
            if (dialPosition == 0)
            {
                stopsAtZero++;
            }
        }

        return (stopsAtZero, clicksAtZero);
    }
    
    
    
    
    //helper method to extract operator and operand L50-> (-1, 59) and  R20 -> (1,20)
    private static (int, int) GetDialMovement(string input)
    {
        char direction = input[0];
        int value = int.Parse(input[1..]);

        return direction == 'L' ? (-1, value) : (1, value);
    }
}