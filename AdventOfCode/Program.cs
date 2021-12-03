namespace AdventOfCode
{
    public class Program
    {
        public static async Task Main()
        {
            var day1 = new Day01();
            await day1.ExecuteAsync();

            var day2 = new Day02();
            await day2.ExecuteAsync();

            var day3 = new Day03();
            await day3.ExecuteAsync();
        }
    }
}
