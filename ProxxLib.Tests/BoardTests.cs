namespace ProxxLib.Tests
{
    public class BoardTests
    {
        [Fact]
        public void Constructor_Correctly_Builds_Adjectent_Cells_For_Corner_Cell()
        {
            var board = CreateTestBoard();

            var cell = board[0, 0];

            Assert.Equal(3, cell.AdjacentCells.Count);
            Assert.Contains(board[1, 0], cell.AdjacentCells);
            Assert.Contains(board[1, 1], cell.AdjacentCells);
            Assert.Contains(board[0, 1], cell.AdjacentCells);
        }

        [Fact]
        public void Constructor_Correctly_Builds_Adjectent_Cells_For_Edge_Cell()
        {
            var board = CreateTestBoard();

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
            var board = CreateTestBoard();

            board.OpenCell(0, 0);

            Assert.True(board[0, 0].IsOpen);
            Assert.True(board[0, 1].IsOpen);
            Assert.True(board[1, 0].IsOpen);
            Assert.True(board[1, 1].IsOpen);
        }

        [Fact]
        public void OpenCell_Does_Not_Opens_Adjectent_Cells_If_Hole_Among_Them()
        {
            var board = CreateTestBoard(cells => cells[4] = null);//Cell (1, 1)

            board.OpenCell(2, 0);

            Assert.True(board[2, 0].IsOpen);
            Assert.False(board[1, 0].IsOpen);
            Assert.False(board[1, 1].IsOpen);
            Assert.False(board[2, 1].IsOpen);
        }

        [Fact]
        public void OpenCell_Sets_GameStatus_To_Won()
        {
            var board = CreateTestBoard();

            board.OpenCell(1, 1);

            Assert.Equal(GameState.Won, board.GameState);
        }

        [Fact]
        public void OpenCell_Sets_GameStatus_To_Lost()
        {
            var cellValues = GetTestCellValues();
            cellValues[0] = null;
            var board = CreateTestBoard(cells => cells[0] = null);

            board.OpenCell(0, 0);

            Assert.Equal(GameState.Lost, board.GameState);
        }

        private Board CreateTestBoard(Action<int?[]>? action = null)
        {
            var cellValues = GetTestCellValues();
            action?.Invoke(cellValues);
            return new(cellValues);
        }

        private int?[] GetTestCellValues() => new int?[]
            {
                1, 2, 3,
                4, 5, 6,
                7, 8, 9
            };
    }
}