namespace AdventOfCode
{
    public record GroundPoint : Point
    {
        public GroundPoint(int x, int y, int value) : base(x, y)
        {
            Value = value;
        }

        public void SetValue(int value) => Value = value;

        public int Value { get; private set; }
    }
}
