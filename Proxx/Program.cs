using ProxxLib;
using System.Runtime.CompilerServices;
using System.Text;

var cellValues = new int?[] 
{ 
    1, 2, 3,
    4, null, 6,
    7, 8, null,
};

var board = new Board(cellValues);

DrawBoard(board);

while (board.GameState == GameState.InProgress)
{
    (var column, var row) = ReadUserInput();
    board.OpenCell(column, row);
    DrawBoard(board);
}

Console.WriteLine($"You {board.GameState}");

void DrawBoard(Board board)
{
    var sb = new StringBuilder();
    
    for (int row = 0; row < board.Size; row++)
    {
        sb.Append('|');

        for (int column = 0; column < board.Size; column++)
        {
            var cellVal = GetDisplayValueForCell(board[column, row]);
            sb.AppendFormat("{0}|", cellVal);
        }

        sb.AppendLine();
    }

    Console.Write(sb.ToString());
}

string GetDisplayValueForCell(Cell cell)
{
    if (!cell.IsOpen)
    {
        return " ";
    }

    if (cell.IsHole)
    {
        return "X";
    }

    return cell.Value.ToString()!;
}

(int column, int row) ReadUserInput()
{
    Console.Write("Enter column number: ");
    var column = int.Parse(Console.ReadLine()!);
    Console.Write("Enter row number: ");
    var row = int.Parse(Console.ReadLine()!);

    return (column, row);
}