namespace BattleCoder.Map
{
    public readonly struct GridPosition
    {
        public readonly int X;
        public readonly int Y;
        public GridPosition(int x, int y) => (X, Y) = (x, y);

        public override string ToString()
        {
            return $"(X: {X}, Y:{Y})";
        }
    }
}