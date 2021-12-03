using System.Text;

namespace AdventOfCode
{
    public class Day03
    {
        public async Task ExecuteAsync()
        {
            var lines = await File.ReadAllLinesAsync("03\\input.txt");
            long g = 0;
            long e = 0;

            StringBuilder sb = new();
            Dictionary<int, string> columnValuesDict = new();
            string columnMostCommonTotal = string.Empty;
            string columnLeastCommonTotal = string.Empty;

            int lineLength = lines[0].Length;

            for (int i = 0; i < lineLength; i++)
            {
                var result = FindMostCommon(lines.ToArray(), i);

                switch (result)
                {
                    case CommonNumberResult.Zeroes:
                        columnMostCommonTotal += "0";
                        columnLeastCommonTotal += "1";
                        break;
                    case CommonNumberResult.Ones:
                        columnMostCommonTotal += "1";
                        columnLeastCommonTotal += "0";
                        break;
                }
            }

            g = Convert.ToInt32(columnMostCommonTotal, 2);
            e = Convert.ToInt32(columnLeastCommonTotal, 2);

            Console.WriteLine($"Task #1: Consumption={g * e}");

            int o, c;
            List<string> oxygenColumns = new(lines);
            List<string> co2Columns = new(lines);
            lineLength = lines[0].Length;

            for (int i = 0; i < lineLength; i++)
            {
                if (oxygenColumns.Count == 1)
                {
                    break;
                }

                var result = FindMostCommon(oxygenColumns.ToArray(), i);

                if (result == CommonNumberResult.Ones)
                {
                    oxygenColumns.RemoveAll(str => str[i] == '0');
                    continue;
                }

                oxygenColumns.RemoveAll(str => str[i] == '1');
            }

            for (int i = 0; i < lineLength; i++)
            {
                if (co2Columns.Count == 1)
                {
                    break;
                }

                var result = FindMostCommon(co2Columns.ToArray(), i);

                if (result == CommonNumberResult.Ones)
                {
                    co2Columns.RemoveAll(str => str[i] == '1');
                    continue;
                }

                co2Columns.RemoveAll(str => str[i] == '0');
            }

            o = Convert.ToInt32(oxygenColumns.First(), 2);
            c = Convert.ToInt32(co2Columns.First(), 2);

            Console.WriteLine($"Task #2: LifeSupportRating={o * c}");
        }

        private CommonNumberResult FindMostCommon(string[] list, int position)
        {
            int zeroCount = 0;
            int oneCount = 0;

            foreach (string str in list)
            {
                if (str[position] == '1')
                {
                    oneCount++;
                }
                else
                {
                    zeroCount++;
                }
            }

            if (oneCount > zeroCount || oneCount == zeroCount)
            {
                return CommonNumberResult.Ones;
            }

            return CommonNumberResult.Zeroes;
        }
    }

    enum CommonNumberResult
    {
        Zeroes,
        Ones
    }
}
