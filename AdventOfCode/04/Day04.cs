using System.Text.Json;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day04
    {
        public async Task ExecuteAsync()
        {
            var lines = await File.ReadAllLinesAsync("04\\input.txt");

            BingoGame bingoGame = InitializeBingoGame(lines);

            var firstWinningCard = bingoGame.FindFirstWinningCard();

            if (firstWinningCard != null)
            {
                var sumOfUnchecked = firstWinningCard.Fields.Where(x => !x.Checked).Select(x => x.Value).Sum();
                Console.WriteLine($"Task #1: {sumOfUnchecked * bingoGame.CurrentNumber}");
            }
              
            bingoGame = InitializeBingoGame(lines);

            var (lastWinningNumber, lastWinningCard) = bingoGame.FindLastWinningCard();

            if (lastWinningCard != null)
            {
                var sumOfUnchecked = lastWinningCard.Fields.Where(x => !x.Checked).Select(x => x.Value).Sum();
                Console.WriteLine($"Task #2: {sumOfUnchecked * lastWinningNumber}");
            }
        }

        private static BingoGame InitializeBingoGame(string[] lines)
        {
            BingoGame bingoGame = new BingoGame();

            int lineIndex = 0;
            var newCard = false;
            int row = 0;

            List<BingoField> currentCardFields = new();
            List<BingoCard> bingoCards = new();

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    newCard = true;
                    currentCardFields = new();
                    row = 0;
                    lineIndex++;
                    continue;
                }

                if (lineIndex == 0)
                {
                    string[] splittedNumbers = line.Split(new char[] { ',' });

                    foreach (var number in splittedNumbers)
                    {
                        bingoGame.Numbers.Enqueue(int.Parse(number));
                    }

                    lineIndex++;
                    continue;
                }

                if (newCard)
                {
                    Regex numberRegex = new Regex(@"\d+");
                    var matches = numberRegex.Matches(line);

                    int matchIndex = 0;
                    foreach (Match match in matches)
                    {
                        currentCardFields.Add(new BingoField(row, matchIndex, int.Parse(match.Value)));
                        matchIndex++;
                    }

                    row++;
                }

                if (currentCardFields.Count == 25)
                {
                    bingoCards.Add(new BingoCard(currentCardFields));
                }

                lineIndex++;
            }

            bingoGame.Cards = bingoCards;

            return bingoGame;
        }
    }
}
