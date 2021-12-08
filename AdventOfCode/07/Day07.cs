namespace AdventOfCode
{
    public class Day07
    {
        public async Task ExecuteAsync()
        {
            IEnumerable<int> submarineState = await ParseInputAsync();

            int min = submarineState.Min();
            int max = submarineState.Max();

            Dictionary<int, int> fuelCostPerPosition = new();
            for (int i = min; i < max; i++)
            {
                foreach (var item in submarineState)
                {
                    if (fuelCostPerPosition.ContainsKey(i))
                    {
                        fuelCostPerPosition[i] += Math.Abs(item - i);
                    }
                    else
                    {
                        fuelCostPerPosition.Add(i, Math.Abs(item - i));
                    }
                }
            }

            Console.WriteLine($"Task #1: {fuelCostPerPosition.OrderBy(x => x.Value).First().Value}");

            fuelCostPerPosition = new();
            for (int i = min; i < max; i++)
            {
                foreach (var item in submarineState)
                {
                    var distance = Math.Abs(item - i);
                    var fuel = distance * (distance + 1) / 2;

                    if (fuelCostPerPosition.ContainsKey(i))
                    {
                        fuelCostPerPosition[i] += fuel;
                    }
                    else
                    {
                        fuelCostPerPosition.Add(i, fuel);
                    }
                }
            }

            Console.WriteLine($"Task #2: {fuelCostPerPosition.OrderBy(x => x.Value).First().Value}");
        }

        private static async Task<IEnumerable<int>> ParseInputAsync()
        {
            List<Fish> fishes = new();

            var lines = await File.ReadAllLinesAsync("07\\input.txt");

            var initialState = lines[0].Split(",").Select(int.Parse);

            return initialState;
        }
    }
}
