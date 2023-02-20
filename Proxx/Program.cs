using ProxxLib;
using System.Text;

//var cellValues = new int?[] 
//{ 
//    1, 2, 3,
//    4, 5, 6,
//    7, 8, 9,
//};

var cellValues = GenerateCellValues(3, 3);

var board = new Board(cellValues);

DrawBoard(board);

while (board.GameState == GameState.InProgress)
{
    (var column, var row) = ReadUserInput();
    board.OpenCell(column, row);
    DrawBoard(board);
}

Console.WriteLine($"You {board.GameState}");

int?[] GenerateCellValues(int boardSize, int holesCount)
{
    var cellValues = Enumerable.Repeat(0, boardSize * boardSize)
        .Select(_ => (int?)Random.Shared.Next(0, 4))
        .ToArray();

    var indicies = Enumerable.Range(0, cellValues.Length).ToList();

    for (int i = 0; i < holesCount; i++)
    {
        var holeIndex = Random.Shared.Next(0, indicies.Count - 1);
        cellValues[indicies[holeIndex]] = null;
        indicies.RemoveAt(holeIndex);
    }

    return cellValues;
}

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
    Console.Write("Enter column number (1-3): ");
    var column = int.Parse(Console.ReadLine()!);
    Console.Write("Enter row number (1-3): ");
    var row = int.Parse(Console.ReadLine()!);

    return (column - 1, row - 1);
}