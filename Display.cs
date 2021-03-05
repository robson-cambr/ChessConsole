using System;
using System.Collections.Generic;

namespace ChessConsole
{
    class Display
    {
        public static void printMatch(ChessMatch chessmatch)
        {
            printChessBoard(chessmatch.ChessBoard);
            Console.Write(Environment.NewLine);
            PrintCapturedPiece(chessmatch);
            Console.WriteLine();
            Console.WriteLine("Shift: " + chessmatch.Shift);
            if (!chessmatch.End)
            {
                Console.WriteLine("Waiting Move: " + chessmatch.CurrentPlayer);
                if (chessmatch.Check)
                {
                    Console.WriteLine("CHECK!");
                }
            }
            else
            {
                Console.WriteLine("CHECKMATE!");
                Console.WriteLine("Winner: " + chessmatch.CurrentPlayer);
            }
        }
        public static void PrintCapturedPiece(ChessMatch chessMatch)
        {
            Console.WriteLine("Captured Pieces");
            Console.Write("White: ");
            PrintByTeam(chessMatch.PieceCapturedByTeam(Coloring.White));
            Console.WriteLine();
            Console.Write("Black: ");
            ConsoleColor standardForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintByTeam(chessMatch.PieceCapturedByTeam(Coloring.Black));
            Console.ForegroundColor = standardForegroundColor;
            Console.WriteLine();
        }
        public static void PrintByTeam(HashSet<ChessPiece> byTeam)
        {
            Console.Write("[");
            foreach (ChessPiece chessPiece in byTeam)
            {
                Console.Write($"{chessPiece} ");
            }
            Console.Write("]");
        }
        public static void printChessBoard(ChessBoard chessBoard)
        {
            for (int i = 0; i < chessBoard.Ranks; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < chessBoard.Files; j++)
                {
                    PrintPiece(chessBoard.PieceOnBoard(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void printAvaliableCoordinatesToMove(ChessBoard chessBoard, bool[,] avaliableCoordinate)
        {
            ConsoleColor standardBackgroundColor = Console.BackgroundColor;
            ConsoleColor avaliableCoordinateBackGroundColor = ConsoleColor.DarkGray;
            for (int i = 0; i < chessBoard.Ranks; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < chessBoard.Files; j++)
                {
                    if (avaliableCoordinate[i, j])
                    {
                        Console.BackgroundColor = avaliableCoordinateBackGroundColor;
                    }
                    else
                    {
                        Console.BackgroundColor = standardBackgroundColor;
                    }
                    PrintPiece(chessBoard.PieceOnBoard(i, j));
                    Console.BackgroundColor = standardBackgroundColor;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = standardBackgroundColor;
        }
        public static BoardMapping BoardMapping()
        {
            string str = Console.ReadLine();
            char file = str[0];
            int rank = int.Parse(str[1] + "");
            return new BoardMapping(file, rank);
        }
        public static void PrintPiece(ChessPiece chessPiece)
        {
            if (chessPiece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (chessPiece.Coloring == Coloring.White)
                {
                    Console.Write(chessPiece);
                }
                else
                {
                    ConsoleColor standardForegroundColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(chessPiece);
                    Console.ForegroundColor = standardForegroundColor;
                }
                Console.Write(" ");
            }
        }
    }
}
