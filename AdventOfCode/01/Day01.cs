namespace AdventOfCode
{
    public class Day01
    {
        public async Task ExecuteAsync()
        {
            var lines = await File.ReadAllLinesAsync("01\\input.txt");

            int? previousValue = null;
            int inc = 0;
            List<int> values = new();

            foreach (var line in lines)
            {
                int lineValue = int.Parse(line);
                values.Add(lineValue);

                if (previousValue == null)
                {
                    previousValue = lineValue;
                    continue;
                }

                if (lineValue > previousValue)
                {
                    inc++;
                }

                previousValue = lineValue;
            }

            Console.WriteLine($"Task #1: {inc}");

            inc = 0;
            int i = 0;
            int? previousSum = null;
            while (true)
            {
                if (i > values.Count)
                {
                    break;
                }

                var currentSum = values.Skip(i).Take(3).Sum();
                i++;

                if (previousSum == null)
                {
                    previousSum = currentSum;
                    continue;
                }

                if (currentSum > previousSum)
                {
                    inc++;
                }

                previousSum = currentSum;
            }

            Console.WriteLine($"Task #2: {inc}");
        }
    }
}
