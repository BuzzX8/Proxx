namespace ProxxLib
{
    public class Board
    {
        private readonly Cell[] cells;

        public Board(IEnumerable<int?> cellValues)
        {
            Size = (int)Math.Sqrt(cellValues.Count());
            cells = cellValues.Select(value => new Cell { Value = value }).ToArray();
            AttachAdjectentCells();
        }

        private void AttachAdjectentCells()
        {
            var adjectencyOffsets = new[]
            {
                (-1, -1), (-1, 0), (-1, 1),
                (0, -1), (0, 1),
                (1, -1), (1, 0), (1, 1)
            };

            for (int i = 0; i < cells.Length; i++)
            {
                var column = i / Size;
                var row = i % Size;
                var cell = cells[i];

                foreach (var adjIdx in adjectencyOffsets)
                {
                    var adjColumn = column + adjIdx.Item1;
                    var adjRow = row + adjIdx.Item2;

                    if (adjColumn < 0 || adjColumn >= Size || adjRow < 0 || adjRow >= Size)
                    {
                        continue;
                    }

                    cell.AdjacentCells.Add(this[adjColumn, adjRow]);
                }
            }
        }

        public Cell this[int columnm, int row]
        {
            get
            {
                var index = Size * columnm + row;

                return cells[index];
            }
        }

        public GameState GameState
        {
            get
            {
                if (cells.Any(cell => cell.IsOpen && cell.IsHole))
                {
                    return GameState.Lost;
                }

                if (cells.Where(cell => !cell.IsHole).All(cell => cell.IsOpen))
                {
                    return GameState.Won;
                }

                return GameState.InProgress;
            }
        }

        public int Size { get; }

        public void OpenCell(int column, int row)
        {
            if (column < 0 || column >= Size || row < 0 || row >= Size)
            {
                throw new ArgumentException();
            }

            if (GameState != GameState.InProgress)
            {
                throw new InvalidOperationException();
            }

            var cell = this[column, row];

            OpenCell(cell);
        }

        private void OpenCell(Cell cell)
        {
            cell.IsOpen = true;

            if (cell.IsHole)
            {
                OpenAllHoles();
                return;
            }

            if (cell.AdjacentCells.Any(c => c.IsHole))
            {
                return;
            }

            foreach (var adjCell in cell.AdjacentCells)
            {
                adjCell.IsOpen = true;
            }
        }

        private void OpenAllHoles()
        {
            foreach (var cell in cells.Where(c => c.IsHole))
            {
                cell.IsOpen = true;
            }
        }
    }
}
