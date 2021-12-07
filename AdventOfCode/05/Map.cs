namespace AdventOfCode
{
    public class Map
    {
        public Dictionary<(int, int), int> Points { get; set; } = new();

        public Map(int height)
        {
            Height = height;
            Width = height;
        }

        public int Height { get; }
        public int Width { get; }

        internal void PlaceLine(Line line)
        {
            LineMovement movement = new LineMovement();

            if (line.From.X == line.To.X)
            {
                movement = LineMovement.Vertical;
            }
            else if (line.From.Y == line.To.Y)
            {
                movement = LineMovement.Horizontal;
            }
            else
            {
                movement = LineMovement.Diagonal;
            }


            switch (movement)
            {
                case LineMovement.Vertical:
                    for (int i = Math.Min(line.From.Y, line.To.Y); i <= Math.Max(line.From.Y, line.To.Y); i++)
                    {
                        if (Points.ContainsKey((line.From.X, i)))
                        {
                            Points[(line.From.X, i)]++;
                        }
                        else
                        {
                            Points.Add((line.From.X, i), 1);
                        }
                    }
                    break;
                case LineMovement.Horizontal:
                    for (int i = Math.Min(line.From.X, line.To.X); i <= Math.Max(line.From.X, line.To.X); i++)
                    {
                        if (Points.ContainsKey((i, line.From.Y)))
                        {
                            Points[(i, line.From.Y)]++;
                        }
                        else
                        {
                            Points.Add((i, line.From.Y), 1);
                        }
                    }
                    break;
                case LineMovement.Diagonal:
                    var x = line.From.X;
                    var y = line.From.Y;

                    var xGoal = line.To.X;
                    var yGoal = line.To.Y;

                    if (Points.ContainsKey((x, y)))
                    {
                        Points[(x, y)]++;
                    }
                    else
                    {
                        Points.Add((x, y), 1);
                    }

                    int stepValX = 1;
                    int stepValY = 1;

                    if (x > xGoal)
                    {
                        stepValX = -1;
                    }

                    if (y > yGoal)
                    {
                        stepValY = -1;
                    }

                    do
                    {
                        x += stepValX;
                        y += stepValY;

                        if (Points.ContainsKey((x, y)))
                        {
                            Points[(x, y)]++;
                        }
                        else
                        {
                            Points.Add((x, y), 1);
                        }
                    } while (x != xGoal || y != yGoal);

                    break;
            }
        }
    }
}
