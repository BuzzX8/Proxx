namespace ProxxLib
{
    public class Board
    {
        public Board(IEnumerable<int?> cellValues)
        {
            Cells = cellValues.Select(value => new Cell { Value = value }).ToArray();
        }

        public IReadOnlyCollection<Cell> Cells { get; set; }

        public GameState GameState
        {
            get
            {
                if (Cells.Any(cell => cell.IsOpen && cell.IsHole))
                {
                    return GameState.Lost;
                }

                if (Cells.Where(cell => !cell.IsHole).All(cell => cell.IsOpen))
                {
                    return GameState.Won;
                }

                return GameState.InProgress;
            }
        }

        public void OpenCell(int column, int row)
        {
            throw new NotImplementedException();
        }
    }
}
