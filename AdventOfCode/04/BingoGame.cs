namespace AdventOfCode
{
    public class BingoGame
    {
        public List<BingoCard> Cards { get; set; } = new();
        public Queue<int> Numbers { get; set; } = new();
        public int CurrentNumber { get; set; }

        public BingoCard? FindFirstWinningCard()
        {
            bool bingo = false;
            BingoCard? wonCard = null;

            while (Numbers.Count > 0)
            {
                CurrentNumber = Numbers.Dequeue();
                foreach (var card in Cards)
                {
                    card.CheckNumber(CurrentNumber);

                    if (card.CheckBingo())
                    {
                        bingo = true;
                        wonCard = card;
                        break;
                    }
                }

                if (bingo)
                {
                    break;
                }
            }

            return wonCard;
        }

        public (int WinningNumber, BingoCard? Card) FindLastWinningCard()
        {
            BingoCard? wonCard = null;
            int lastBingoNumber = 0;

            while (Numbers.Count > 0)
            {
                CurrentNumber = Numbers.Dequeue();
                foreach (var card in Cards)
                {
                    card.CheckNumber(CurrentNumber);

                    if (card.CheckBingo())
                    {
                        wonCard = card;
                        lastBingoNumber = CurrentNumber;
                        continue;
                    }
                }
            }

            return (lastBingoNumber, wonCard);
        }
    }
}
