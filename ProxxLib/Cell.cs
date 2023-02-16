namespace ProxxLib
{
    public class Cell
    {
        public bool IsOpen { get; set; }

        public bool IsHole => Value is null;

        /// <summary>
        /// null means hole.
        /// </summary>
        public int? Value { get; set; }

        public List<Cell> AdjacentCells { get; } = new List<Cell>();
    }
}