namespace AdventOfCode
{
    public class BingoField
    {
        public BingoField(int row, int column, int value)
        {
            Row = row;
            Column = column;
            Value = value;
        }

        public void Check()
        {
            Checked = true;
        }

        public int Row { get; }
        public int Column { get; }
        public int Value { get; }
        public bool Checked { get; set; } = false;
    }
}
