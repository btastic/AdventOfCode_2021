namespace AdventOfCode
{
    public class Day02
    {
        public async Task ExecuteAsync()
        {
            var lines = await File.ReadAllLinesAsync("02\\input.txt");
            int h = 0;
            int d = 0;
            int a = 0;

            foreach (var line in lines)
            {
                int amt = int.Parse(line.Last().ToString());
                switch (line[0])
                {
                    case 'f':
                        h += amt;
                        break;
                    case 'd':
                        d += amt;
                        break;
                    case 'u':
                        d -= amt;
                        break;
                }
            }

            Console.WriteLine($"Task #1: {h * d}");

            h = 0;
            d = 0;
            a = 0;
            foreach (var line in lines)
            {
                int amt = int.Parse(line.Last().ToString());
                switch (line[0])
                {
                    case 'f':
                        h += amt;
                        d += a * amt;
                        break;
                    case 'd':
                        a += amt;
                        break;
                    case 'u':
                        a -= amt;
                        break;
                }
            }

            Console.WriteLine($"Task #2: {h * d}");
        }
    }
}
