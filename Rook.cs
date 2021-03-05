namespace ChessConsole
{
    class Rook : ChessPiece
    {
        public Rook(ChessBoard chessBoard, Coloring coloring) : base(chessBoard, coloring)
        {
        }
        public override string ToString()
        {
            return "R";
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
                if (ChessBoard.PieceOnBoard(coordinate) != null && ChessBoard.PieceOnBoard(coordinate).Coloring != Coloring)
                {
                    break;
                }
                coordinate.Rank -= 1;
            }
            // South
            coordinate.GetValue(Coordinate.Rank + 1, Coordinate.File);
            while (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
                if (ChessBoard.PieceOnBoard(coordinate) != null && ChessBoard.PieceOnBoard(coordinate).Coloring != Coloring)
                {
                    break;
                }
                coordinate.Rank += 1;
            }
            // East
            coordinate.GetValue(Coordinate.Rank, Coordinate.File + 1);
            while (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
                if (ChessBoard.PieceOnBoard(coordinate) != null && ChessBoard.PieceOnBoard(coordinate).Coloring != Coloring)
                {
                    break;
                }
                coordinate.File += 1;
            }
            // West
            coordinate.GetValue(Coordinate.Rank, Coordinate.File - 1);
            while (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
                if (ChessBoard.PieceOnBoard(coordinate) != null && ChessBoard.PieceOnBoard(coordinate).Coloring != Coloring)
                {
                    break;
                }
                coordinate.File -= 1;
            }
            return movePiece;
        }
    }
}
