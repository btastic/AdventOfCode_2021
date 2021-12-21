namespace AdventOfCode
{
    public class Day08
    {
        public class InputLine
        {
            public InputLine(string signals, string digits)
            {
                Signals = signals.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                Digits = digits.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            }

            public string[] Signals { get; }
            public string[] Digits { get; }
        }
        public class Segment
        {
            public List<int> PossibleNumbers { get; set; } = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            public bool NeedsCalculation { get => PossibleNumbers.Count != 1; }
            public int? Value { get => PossibleNumbers.Count == 1 ? PossibleNumbers.First() : null; }
            public string Digits { get; }

            public Segment(string digits)
            {
                Digits = digits;

                switch (Digits.Length)
                {
                    case 1:
                        throw new ArgumentOutOfRangeException("Not a valid digit");
                    case 2:
                        PossibleNumbers.Clear();
                        PossibleNumbers.Add(1);
                        break;
                    case 3:
                        PossibleNumbers.Clear();
                        PossibleNumbers.Add(7);
                        break;
                    case 4:
                        PossibleNumbers.Clear();
                        PossibleNumbers.Add(4);
                        break;
                    case 5:
                        PossibleNumbers.Remove(0);
                        PossibleNumbers.Remove(1);
                        PossibleNumbers.Remove(4);
                        PossibleNumbers.Remove(6);
                        PossibleNumbers.Remove(8);
                        PossibleNumbers.Remove(9);
                        break;
                    case 6:
                        PossibleNumbers.Remove(1);
                        PossibleNumbers.Remove(2);
                        PossibleNumbers.Remove(4);
                        PossibleNumbers.Remove(5);
                        PossibleNumbers.Remove(7);
                        PossibleNumbers.Remove(8);
                        break;
                    case 7:
                        PossibleNumbers.Clear();
                        PossibleNumbers.Add(8);
                        break;
                }
            }

            public void CalculatePossibleNumbers(
                List<Segment> otherSegments,
                Dictionary<char, char> mapping)
            {
                if (!NeedsCalculation)
                {
                    if (Value != null)
                    {
                        switch (Value)
                        {
                            case 1:
                                mapping.TryAdd('c', Digits[0]);
                                mapping.TryAdd('f', Digits[1]);
                                break;
                            case 4:
                                mapping.TryAdd('b', Digits[0]);
                                mapping.TryAdd('c', Digits[1]);
                                mapping.TryAdd('d', Digits[2]);
                                mapping.TryAdd('f', Digits[3]);
                                break;
                            case 7:
                                mapping.TryAdd('a', Digits[0]);
                                mapping.TryAdd('c', Digits[1]);
                                mapping.TryAdd('f', Digits[2]);
                                break;
                            case 8:
                                mapping.TryAdd('a', Digits[0]);
                                mapping.TryAdd('b', Digits[1]);
                                mapping.TryAdd('c', Digits[2]);
                                mapping.TryAdd('d', Digits[3]);
                                mapping.TryAdd('e', Digits[4]);
                                mapping.TryAdd('f', Digits[5]);
                                mapping.TryAdd('g', Digits[6]);
                                break;
                        }
                    }

                    return;
                }

                foreach (var doneSegments in otherSegments.Where(x => !x.NeedsCalculation))
                {
                    PossibleNumbers.Remove(doneSegments.PossibleNumbers.First());
                }
            }
        }

        public async Task ExecuteAsync()
        {
            List<InputLine> input = await ParseInputAsync();

            int timesDisplayed = 0;
            foreach (var digit in input.Select(x => x.Digits).SelectMany(x => x))
            {
                switch (digit.Length)
                {
                    case 2:
                        timesDisplayed++;
                        break;
                    case 3:
                        timesDisplayed++;
                        break;
                    case 4:
                        timesDisplayed++;
                        break;
                    case 7:
                        timesDisplayed++;
                        break;
                }
            }

            Console.WriteLine($"Task #1: {timesDisplayed}");

            Dictionary<char, char> mapping = new();

            List<Segment> segments = new();
            mapping = CreateMapping(input.Select(x => x.Signals).First());
        }

        private Dictionary<char, char> CreateMapping(string[] signals)
        {
            var one = signals.FirstOrDefault(x => x.Length == 2);
            var four = signals.FirstOrDefault(x => x.Length == 4);
            var seven = signals.FirstOrDefault(x => x.Length == 3);
            var eight = signals.FirstOrDefault(x => x.Length == 7);

            var nine = signals.FirstOrDefault(x => x.Length == 6 && x.Except(seven).Except(four).Count() == 1);
            var six = signals.FirstOrDefault(x => x.Length == 6 && x != nine && one.Except(x).Count() == 1);
            var zero = signals.FirstOrDefault(x => x.Length == 6 && x != nine && x != six);

            var c = eight.Except(six).FirstOrDefault();
            var e = eight.Except(nine).FirstOrDefault();
            var f = eight.Except(one).Except(new[] { c }).FirstOrDefault();

        }

        private static async Task<List<InputLine>> ParseInputAsync()
        {
            List<InputLine> values = new();

            var lines = await File.ReadAllLinesAsync("08\\testinput.txt");

            foreach (var line in lines)
            {
                values.Add(new InputLine(
                    line.Split("|", StringSplitOptions.RemoveEmptyEntries)[0],
                    line.Split("|", StringSplitOptions.RemoveEmptyEntries)[1]));
            }

            return values;
        }
    }
}
