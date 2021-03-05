namespace ChessConsole
{
    class Queen : ChessPiece
    {
        public Queen(ChessBoard chessBoard, Coloring coloring) : base(chessBoard, coloring)
        {
        }
        public override string ToString()
        {
            return "Q";
        }
        private bool AdversaryOrEmpty(Coordinate coordinate)
        {
            ChessPiece piece = ChessBoard.PieceOnBoard(coordinate);
            return piece == null || piece.Coloring != Coloring;
        }
        public override bool[,] PieceMoveSet()
        {
            bool[,] movePiece = new bool[ChessBoard.Ranks, ChessBoard.Files];
            Coordinate coordinate = new Coordinate(0, 0);

            // North
            coordinate.GetValue(Coordinate.Rank - 1, Coordinate.File);
            while (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
                if (ChessBoard.PieceOnBoard(coordinate) != null &&
                    ChessBoard.PieceOnBoard(coordinate).Coloring != Coloring)
                {
                    break;
                }
                coordinate.GetValue(coordinate.Rank - 1, coordinate.File);
            }
            // Northeast
            coordinate.GetValue(Coordinate.Rank - 1, Coordinate.File + 1);
            while (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
                if (ChessBoard.PieceOnBoard(coordinate) != null &&
                    ChessBoard.PieceOnBoard(coordinate).Coloring != Coloring)
                {
                    break;
                }
                coordinate.GetValue(coordinate.Rank - 1, coordinate.File + 1);
            }
            // East
            coordinate.GetValue(Coordinate.Rank, Coordinate.File + 1);
            while (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
                if (ChessBoard.PieceOnBoard(coordinate) != null &&
                    ChessBoard.PieceOnBoard(coordinate).Coloring != Coloring)
                {
                    break;
                }
                coordinate.GetValue(coordinate.Rank, coordinate.File + 1);
            }
            // Southeast
            coordinate.GetValue(Coordinate.Rank + 1, Coordinate.File + 1);
            while (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
                if (ChessBoard.PieceOnBoard(coordinate) != null &&
                    ChessBoard.PieceOnBoard(coordinate).Coloring != Coloring)
                {
                    break;
                }
                coordinate.GetValue(coordinate.Rank + 1, coordinate.File + 1);
            }
            // South
            coordinate.GetValue(Coordinate.Rank + 1, Coordinate.File);
            while (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
                if (ChessBoard.PieceOnBoard(coordinate) != null &&
                    ChessBoard.PieceOnBoard(coordinate).Coloring != Coloring)
                {
                    break;
                }
                coordinate.GetValue(coordinate.Rank + 1, coordinate.File);
            }
            // Southwest
            coordinate.GetValue(Coordinate.Rank + 1, Coordinate.File - 1);
            while (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
                if (ChessBoard.PieceOnBoard(coordinate) != null &&
                    ChessBoard.PieceOnBoard(coordinate).Coloring != Coloring)
                {
                    break;
                }
                coordinate.GetValue(coordinate.Rank + 1, coordinate.File - 1);
            }
            // West
            coordinate.GetValue(Coordinate.Rank, Coordinate.File - 1);
            while (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
                if (ChessBoard.PieceOnBoard(coordinate) != null &&
                    ChessBoard.PieceOnBoard(coordinate).Coloring != Coloring)
                {
                    break;
                }
                coordinate.GetValue(coordinate.Rank, coordinate.File - 1);
            }
            
            // Northwest
            coordinate.GetValue(Coordinate.Rank - 1, Coordinate.File - 1);
            while (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
                if (ChessBoard.PieceOnBoard(coordinate) != null &&
                    ChessBoard.PieceOnBoard(coordinate).Coloring != Coloring)
                {
                    break;
                }
                coordinate.GetValue(coordinate.Rank - 1, coordinate.File - 1);
            }
            return movePiece;
        }
    }
}
