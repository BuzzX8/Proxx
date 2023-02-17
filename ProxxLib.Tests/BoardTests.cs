namespace ProxxLib.Tests
{
    public class BoardTests
    {
        [Fact]
        public void Constructor_Correctly_Builds_Adjectent_Cells_For_Corner_Cell()
        {
            var cellValues = GetTestCellValues();
            var board = new Board(cellValues);

            var cell = board[0, 0];

            Assert.Equal(3, cell.AdjacentCells.Count);
            Assert.Contains(board[1, 0], cell.AdjacentCells);
            Assert.Contains(board[1, 1], cell.AdjacentCells);
            Assert.Contains(board[0, 1], cell.AdjacentCells);
        }

        [Fact]
        public void Constructor_Correctly_Builds_Adjectent_Cells_For_Edge_Cell()
        {
            var cellValues = GetTestCellValues();
            var board = new Board(cellValues);

            var cell = board[0, 1];

            Assert.Equal(5, cell.AdjacentCells.Count);
            Assert.Contains(board[0, 0], cell.AdjacentCells);
            Assert.Contains(board[1, 0], cell.AdjacentCells);
            Assert.Contains(board[1, 1], cell.AdjacentCells);
            Assert.Contains(board[1, 2], cell.AdjacentCells);
            Assert.Contains(board[0, 2], cell.AdjacentCells);
        }

        [Fact]
        public void OpenCell_Opens_Adjectent_Cells_For_Cell_In_Corner()
        {
            var cellValues = GetTestCellValues();
            var board = new Board(cellValues);

            board.OpenCell(0, 0);

            Assert.True(board[0, 0].IsOpen);
            Assert.True(board[0, 1].IsOpen);
            Assert.True(board[1, 0].IsOpen);
            Assert.True(board[1, 1].IsOpen);
        }

        [Fact]
        public void OpenCell_Does_Not_Opens_Adjectent_Cells_If_Hole_Among_Them()
        {
            var cellValues = GetTestCellValues();
            cellValues[4] = null;
            var board = new Board(cellValues);

            board.OpenCell(2, 0);

            Assert.True(board[2, 0].IsOpen);
            Assert.False(board[1, 0].IsOpen);
            Assert.False(board[1, 1].IsOpen);
            Assert.False(board[2, 1].IsOpen);
        }

        private int?[] GetTestCellValues() => new int?[]
            {
                1, 2, 3,
                4, 5, 6,
                7, 8, 9
            };
    }
}