namespace ChessConsole
{
    class King : ChessPiece
    {
        private ChessMatch ChessMatch;
        public King(ChessBoard chessboard, Coloring coloring, ChessMatch chessMatch) : base(chessboard, coloring)
        {
            ChessMatch = chessMatch;
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
        private bool RookAvaliableToCastling (Coordinate coordinate)
        {
            ChessPiece piece = ChessBoard.PieceOnBoard(coordinate);
            return piece != null && piece is Rook && piece.Coloring == Coloring && piece.MovementQuantity == 0;
        }
        public override bool[,] PieceMoveSet()
        {
            bool[,] movePiece = new bool[ChessBoard.Ranks, ChessBoard.Files];
            Coordinate coordinate = new Coordinate(0, 0);

            // North
            coordinate.GetValue(Coordinate.Rank - 1, Coordinate.File);
            if (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
            }
            // Northeast
            coordinate.GetValue(Coordinate.Rank - 1, Coordinate.File + 1);
            if (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
            }
            // East
            coordinate.GetValue(Coordinate.Rank, Coordinate.File + 1);
            if (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
            }
            // SouthEast
            coordinate.GetValue(Coordinate.Rank + 1, Coordinate.File + 1);
            if (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
            }
            // South
            coordinate.GetValue(Coordinate.Rank + 1, Coordinate.File);
            if (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
            }
            // Southwest
            coordinate.GetValue(Coordinate.Rank + 1, Coordinate.File - 1);
            if (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
            }
            // West
            coordinate.GetValue(Coordinate.Rank, Coordinate.File - 1);
            if (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
            }
            // Northwest
            coordinate.GetValue(Coordinate.Rank - 1, Coordinate.File - 1);
            if (ChessBoard.CoordinateIsValid(coordinate) && AdversaryOrEmpty(coordinate))
            {
                movePiece[coordinate.Rank, coordinate.File] = true;
            }

            // #Castling
            if (MovementQuantity == 0 && !ChessMatch.Check)
            {
                // #Minor Castling
                Coordinate FirstRookCoordinate = new Coordinate(Coordinate.Rank, Coordinate.File + 3);
                if (RookAvaliableToCastling(FirstRookCoordinate))
                {
                    Coordinate FirstRook = new Coordinate(Coordinate.Rank, Coordinate.File + 1);
                    Coordinate SecondRook = new Coordinate(Coordinate.Rank, Coordinate.File + 2);
                    if (ChessBoard.PieceOnBoard(FirstRook) == null && ChessBoard.PieceOnBoard(SecondRook) == null)
                    {
                        movePiece[Coordinate.Rank, Coordinate.File + 2] = true;
                    }
                }
                // #Great Castling
                Coordinate SecondRookCoordinate = new Coordinate(Coordinate.Rank, Coordinate.File - 4);
                if (RookAvaliableToCastling(SecondRookCoordinate))
                {
                    Coordinate p1 = new Coordinate(Coordinate.Rank, Coordinate.File - 1);
                    Coordinate p2 = new Coordinate(Coordinate.Rank, Coordinate.File - 2);
                    Coordinate p3 = new Coordinate(Coordinate.Rank, Coordinate.File - 3);
                    if (ChessBoard.PieceOnBoard(p1) == null && ChessBoard.PieceOnBoard(p2) == null && ChessBoard.PieceOnBoard(p3) == null)
                    {
                        movePiece[Coordinate.Rank, Coordinate.File - 2] = true;
                    }
                }
            }
            return movePiece;
        }
    }
}
