namespace ProxxLib
{
    public class Board
    {
        public Board(IEnumerable<int?> cellValues)
        {
            Size = (int)Math.Sqrt(cellValues.Count());
            Cells = cellValues.Select(value => new Cell { Value = value }).ToArray();
            AttachAdjectentCells();
        }

        private void AttachAdjectentCells()
        {
            var adjectencyIndicies = new[]
            {
                (-1, -1), (-1, 0), (-1, 1),
                (0, -1), (0, 1),
                (1, -1), (1, 0), (1, 1)
            };

            for (int i = 0; i < Cells.Count; i++)
            {
                var column = i / Size;
                var row = i % Size;
                var cell = Cells[i];

                foreach (var adjIdx in adjectencyIndicies)
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

                return Cells[index];
            }
        }

        public IReadOnlyList<Cell> Cells { get; set; }

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
            cell.IsOpen = true;

            if (cell.IsHole || cell.AdjacentCells.Any(c => c.IsHole))
            {
                return;
            }

            foreach (var adjCell in cell.AdjacentCells)
            {
                adjCell.IsOpen = true;
            }
        }
    }
}
