using System;

namespace ChessConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch chessMatch = new ChessMatch();
                while (!chessMatch.End)
                {
                    try
                    {
                        Console.Clear();
                        Display.printMatch(chessMatch);

                        Console.WriteLine();
                        Console.Write("Move From: ");
                        Coordinate moveFrom = Display.BoardMapping().ToCoordinate();
                        chessMatch.IsValidMoveFrom(moveFrom);

                        bool[,] avaliableMovement = chessMatch.ChessBoard.PieceOnBoard(moveFrom).PieceMoveSet();

                        Console.Clear();
                        Display.printAvaliableCoordinatesToMove(chessMatch.ChessBoard, avaliableMovement);

                        Console.WriteLine();
                        Console.Write("Move To: ");
                        Coordinate moveTo = Display.BoardMapping().ToCoordinate();
                        chessMatch.IsValidMoveTo(moveFrom, moveTo);
                        chessMatch.ShiftManager(moveFrom, moveTo);
                    }
                    catch (ChessConsoleException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Display.printMatch(chessMatch);
            }
            catch (ChessConsoleException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
