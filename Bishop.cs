namespace ChessConsole
{
    class Bishop : ChessPiece
    {
        public Bishop(ChessBoard chessBoard, Coloring coloring) : base(chessBoard, coloring)
        {
        }
        public override string ToString()
        {
            return "B";
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

            // Northwest
            coordinate.GetValue(Coordinate.Rank - 1, Coordinate.File - 1);
            while (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate) == true)
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
                if (ChessBoard.PieceOnBoard(coordinate) != null &&
                    ChessBoard.PieceOnBoard(coordinate).Coloring != Coloring)
                {
                    break;
                }
                coordinate.GetValue(coordinate.Rank - 1, coordinate.File - 1);
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
            return movePiece;
        }
    }
}
