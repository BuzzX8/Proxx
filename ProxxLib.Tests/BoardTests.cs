namespace ProxxLib.Tests
{
    public class BoardTests
    {
        [Fact]
        public void Constructor_Correctly_Builds_Adjectent_Cells_For_Corner_Cell()
        {
            var cellValues = new int?[] 
            {
                1, 2, 3, 
                4, 5, 6, 
                7, 8, 9 
            };
            var board = new Board(cellValues);

            var cell = board[0, 0];

            Assert.Equal(3, cell.AdjacentCells.Count);
            Assert.Contains(board[1, 0], cell.AdjacentCells);
            Assert.Contains(board[1, 1], cell.AdjacentCells);
            Assert.Contains(board[0, 1], cell.AdjacentCells);
        }
    }
}