namespace AdventOfCode
{
    public class BingoCard
    {
        public List<BingoField> Fields { get; set; } = new();
        public bool HasBingo { get; set; }

        public BingoCard(List<BingoField> fields)
        {
            Fields = fields;
        }

        public bool CheckBingo()
        {
            if (HasBingo)
            {
                return false;
            }

            return CheckBingoRows() || CheckBingoColumns();
        }

        private bool CheckBingoRows()
        {
            for (int i = 0; i < 5; i++)
            {
                var bingo = Fields.Where(x => x.Row == i).All(x => x.Checked);

                if (bingo)
                {
                    HasBingo = true;
                    return true;
                }
            }

            return false;
        }

        private bool CheckBingoColumns()
        {
            for (int i = 0; i < 5; i++)
            {
                var bingo = Fields.Where(x => x.Column == i).All(x => x.Checked);

                if (bingo)
                {
                    HasBingo = true;
                    return true;
                }
            }

            return false;
        }

        public void CheckNumber(int number)
        {
            if (HasBingo)
            {
                return;
            }

            BingoField? field = Fields.SingleOrDefault(x => x.Value == number);

            if (field == null)
            {
                return;
            }

            field.Check();
        }
    }
}
