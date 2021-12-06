using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public record GroundPoint : Point
    {
        public GroundPoint(int x, int y, int value) : base(x, y)
        {
            Value = value;
        }

        public void SetValue(int value) => Value = value;

        public int Value { get; private set; }
    }

    public record Point(int X, int Y);
    public record Line(Point From, Point To);

    public class Ground
    {
        public List<GroundPoint> Points { get; set; } = new();

        public Ground(int height)
        {
            Height = height;
            Width = height;

            for (int w = 0; w < Width; w++)
            {
                for (int h = 0; h < Height; h++)
                {
                    Points.Add(new GroundPoint(w, h, 0));
                }
            }
        }

        public int Height { get; }
        public int Width { get; }

        internal void PlaceLine(Line line)
        {
            Movement movement = new Movement();

            if (line.From.X == line.To.X)
            {
                movement = Movement.Vertical;
            }
            else
            {
                movement = Movement.Horizontal;
            }

            switch (movement)
            {
                case Movement.Vertical:
                    for (int i = Math.Min(line.From.Y, line.To.Y); i <= Math.Max(line.From.Y, line.To.Y); i++)
                    {
                        var point = Points.Find(x => x.X == line.From.X && x.Y == i);
                        point.SetValue(point.Value + 1);
                    }
                    break;
                case Movement.Horizontal:
                    for (int i = Math.Min(line.From.X, line.To.X); i <= Math.Max(line.From.X, line.To.X); i++)
                    {
                        var point = Points.Find(x => x.Y == line.From.Y && x.X == i);
                        point.SetValue(point.Value + 1);
                    }
                    break;
            }
        }
    }

    enum Movement
    {
        Vertical,
        Horizontal,
        Diagonal
    }

    public class Day05
    {
        public async Task ExecuteAsync()
        {
            var lines = await File.ReadAllLinesAsync("05\\input.txt");
            List<Line> result = ParseInput(lines);

            int maxY = 0;
            int maxX = 0;

            foreach (var line in result)
            {
                maxX = new[] { line.From.X, line.To.X, maxX }.Max();
                maxY = new[] { line.From.Y, line.To.Y, maxY }.Max();
            }

            var ground = new Ground(Math.Max(maxY+1, maxX+1));

            foreach (var line in result.Where(x => (x.From.X == x.To.X) || x.From.Y == x.To.Y))
            {
                ground.PlaceLine(line);


                //WriteDebugFile(ground);
            }

            var relevantPoints = ground.Points.Where(x => x.Value >= 2);
            Console.WriteLine($"Task #1: {relevantPoints.Count()}");

            Console.ReadKey();
        }

        private static void WriteDebugFile(Ground ground)
        {
            StringBuilder sb = new StringBuilder();
            int colCount = 0;
            foreach (var point in ground.Points.OrderBy(x => x.Y))
            {
                if (colCount >= ground.Width)
                {
                    sb.AppendLine();
                    colCount = 0;
                }

                sb.Append(point.Value.ToString() == "0" ? "." : point.Value.ToString());
                colCount++;
            }

            File.WriteAllText("lines.txt", sb.ToString());
        }

        private static List<Line> ParseInput(string[] lines)
        {
            List<Line> result = new List<Line>();
            Regex regex = new Regex(@"(\d+,\d+) -> (\d+,\d+)");

            foreach (var item in lines)
            {
                Point? fromPoint = null;
                Point? toPoint = null;

                var regexResult = regex.Match(item);

                foreach (Group match in regexResult.Groups.Values.Skip(1))
                {
                    var matches = match.Value.ToString().Split(",");

                    if (fromPoint == null)
                    {
                        fromPoint = new Point(Convert.ToInt32(matches[0]), Convert.ToInt32(matches[1]));
                        continue;
                    }

                    if (fromPoint != null && toPoint == null)
                    {
                        toPoint = new Point(Convert.ToInt32(matches[0]), Convert.ToInt32(matches[1]));
                        result.Add(new Line(fromPoint, toPoint));

                        fromPoint = null;
                        toPoint = null;
                    }
                }
            }

            return result;
        }
    }
}
