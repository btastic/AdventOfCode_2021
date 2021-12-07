using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
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

            var map = new Map(Math.Max(maxY + 1, maxX + 1));

            foreach (var line in result.Where(x => (x.From.X == x.To.X) || x.From.Y == x.To.Y))
            {
                map.PlaceLine(line);
                WriteDebugFile(map);
            }

            var relevantPoints = map.Points.Where(x => x.Value >= 2);
            Console.WriteLine($"Task #1: {relevantPoints.Count()}");

            map = new Map(Math.Max(maxY + 1, maxX + 1));
            foreach (var line in result)
            {
                map.PlaceLine(line);
                WriteDebugFile(map);
            }

            relevantPoints = map.Points.Where(x => x.Value >= 2);
            Console.WriteLine($"Task #2: {relevantPoints.Count()}");

            Console.ReadKey();
        }

        private static void WriteDebugFile(Map map)
        {
            StringBuilder sb = new StringBuilder();
            int colCount = 0;
            foreach (var point in map.Points.OrderBy(x => x.Key.Item2))
            {
                if (colCount >= map.Width)
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
