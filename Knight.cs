namespace ChessConsole
{
    class Knight : ChessPiece
    {
        public Knight(ChessBoard chessBoard, Coloring coloring) : base(chessBoard, coloring)
        {
        }
        public override string ToString()
        {
            return "K";
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

            coordinate.GetValue(Coordinate.Rank - 1, Coordinate.File - 2);
            if (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
            }
            coordinate.GetValue(Coordinate.Rank - 2, Coordinate.File - 1);
            if (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
            }
            coordinate.GetValue(Coordinate.Rank - 2, Coordinate.File + 1);
            if (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
            }
            coordinate.GetValue(Coordinate.Rank - 1, Coordinate.File + 2);
            if (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
            }
            coordinate.GetValue(Coordinate.Rank + 1, Coordinate.File + 2);
            if (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
            }
            coordinate.GetValue(Coordinate.Rank + 2, Coordinate.File + 1);
            if (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
            }
            coordinate.GetValue(Coordinate.Rank + 2, Coordinate.File - 1);
            if (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
            }
            coordinate.GetValue(Coordinate.Rank + 1, Coordinate.File - 2);
            if (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
            }
            return movePiece;
        }
    }
}
