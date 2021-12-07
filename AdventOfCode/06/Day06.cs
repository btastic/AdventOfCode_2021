namespace AdventOfCode
{
    public class Fish
    {
        public Fish(int timer)
        {
            Timer = timer;
        }

        public int Timer { get; set; }
    }

    public class Day06
    {
        public async Task ExecuteAsync()
        {
            List<Fish> fishes = await ParseInputAsync();

            List<Fish> newFishes = new();
            for (int i = 1; i <= 80; i++)
            {
                newFishes.Clear();
                foreach (var fish in fishes)
                {
                    if (fish.Timer == 0)
                    {
                        newFishes.Add(new Fish(8));
                        fish.Timer = 6;
                    }
                    else
                    {
                        fish.Timer--;
                    }
                }

                fishes.AddRange(newFishes);
            }
            Console.WriteLine($"Task #1: {fishes.Count()}");

            fishes = await ParseInputAsync();

            var fishAges = new long[9];

            foreach (var fish in fishes)
            {
                fishAges[fish.Timer]++;
            }

            for (int i = 0; i < 256; i++)
            {
                fishAges[(i + 7) % 9] += fishAges[i % 9];
            }

            Console.WriteLine($"Task #2: {fishAges.Sum()}");
        }

        private static async Task<List<Fish>> ParseInputAsync()
        {
            List<Fish> fishes = new();

            var lines = await File.ReadAllLinesAsync("06\\input.txt");

            var initialState = lines[0].Split(",");

            foreach (var state in initialState)
            {
                fishes.Add(new Fish(int.Parse(state)));
            }

            return fishes;
        }

        public void PrintState(int day, List<Fish> fishes)
        {
            Console.WriteLine($"After\t{day} day:\t {string.Join(",", fishes.Select(x => x.Timer))}");
        }
    }
}
