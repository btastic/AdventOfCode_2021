namespace AdventOfCode
{
    public enum Position
    {
        Left,
        Top,
        Right,
        Bottom
    }
    public class HeightPoint
    {
        public int? Left { get; set; } = null;
        public int? Top { get; set; } = null;
        public int? Right { get; set; } = null;
        public int? Bottom { get; set; } = null;
        public int Value { get; }

        public bool ValueIsLowest
        {
            get
            {
                bool isLowest = true;

                if (Left != null)
                {
                    isLowest = Left > Value;
                }

                if (!isLowest)
                {
                    return false;
                }

                if (Right != null)
                {
                    isLowest = Right > Value;
                }

                if (!isLowest)
                {
                    return false;
                }

                if (Top != null)
                {
                    isLowest = Top > Value;
                }

                if (!isLowest)
                {
                    return false;
                }

                if (Bottom != null)
                {
                    isLowest = Bottom > Value;
                }

                return isLowest;
            }

        }

        public HeightPoint(int value)
        {
            Value = value;
        }

        public void SetAdjacent(Position pos, int value)
        {
            switch (pos)
            {
                case Position.Left:
                    Left = value;
                    break;
                case Position.Top:
                    Top = value;
                    break;
                case Position.Right:
                    Right = value;
                    break;
                case Position.Bottom:
                    Bottom = value;
                    break;
            }
        }
    }

    public class Day09
    {

        public async Task ExecuteAsync()
        {
            List<string> lines = await ParseInputAsync();

            int lineIndex = 0;
            List<HeightPoint> heightPoints = new();
            foreach (var line in lines)
            {
                int charIndex = 0;
                foreach (char c in line)
                {
                    int number = int.Parse(c.ToString());
                    var heightPoint = new HeightPoint(number);

                    if (charIndex - 1 >= 0)
                    {
                        heightPoint.SetAdjacent(Position.Left, int.Parse(line[charIndex - 1].ToString()));
                    }

                    if (charIndex + 1 < line.Length)
                    {
                        heightPoint.SetAdjacent(Position.Right, int.Parse(line[charIndex + 1].ToString()));
                    }

                    if (lineIndex + 1 < lines.Count)
                    {
                        heightPoint.SetAdjacent(Position.Bottom, int.Parse(lines[lineIndex + 1][charIndex].ToString()));
                    }

                    if (lineIndex - 1 >= 0)
                    {
                        heightPoint.SetAdjacent(Position.Top, int.Parse(lines[lineIndex - 1][charIndex].ToString()));
                    }

                    heightPoints.Add(heightPoint);
                    charIndex++;
                }

                lineIndex++;
            }

            var result = heightPoints.Where(x => x.ValueIsLowest).Sum(x => x.Value + 1);

            Console.WriteLine($"Task #1: {result}");
        }

        private static async Task<List<string>> ParseInputAsync()
        {
            var lines = await File.ReadAllLinesAsync("09\\input.txt");

            return lines.ToList();
        }
    }
}
