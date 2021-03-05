namespace ChessConsole
{
    class Pawn : ChessPiece
    {
        private ChessMatch ChessMatch;
        public Pawn(ChessBoard chessBoard, Coloring coloring, ChessMatch chessMatch) : base(chessBoard, coloring)
        {
            ChessMatch = chessMatch;
        }
        public override string ToString()
        {
            return "P";
        }
        private bool Empty(Coordinate coordinate)
        {
            return ChessBoard.PieceOnBoard(coordinate) == null;
        }
        private bool AdversaryNearby(Coordinate coordinate)
        {
            ChessPiece piece = ChessBoard.PieceOnBoard(coordinate);
            return piece != null && piece.Coloring != Coloring;
        }
        public override bool[,] PieceMoveSet()
        {
            bool[,] movePiece = new bool[ChessBoard.Ranks, ChessBoard.Files];
            Coordinate coordinate = new Coordinate(0, 0);

            if (Coloring == Coloring.White)
            {
                coordinate.GetValue(Coordinate.Rank - 1, Coordinate.File);
                if (ChessBoard.CoordinateIsValid(coordinate) && Empty(coordinate))
                {
                    movePiece[coordinate.Rank, coordinate.File] = true;
                }
                coordinate.GetValue(Coordinate.Rank - 2, Coordinate.File);
                Coordinate TwoCoordinateForward = new Coordinate(Coordinate.Rank - 1, Coordinate.File);
                if (ChessBoard.CoordinateIsValid(TwoCoordinateForward) && Empty(TwoCoordinateForward) &&
                    ChessBoard.CoordinateIsValid(coordinate) && Empty(coordinate) && MovementQuantity == 0)
                {
                    movePiece[coordinate.Rank, coordinate.File] = true;
                }
                coordinate.GetValue(Coordinate.Rank - 1, Coordinate.File - 1);
                if (ChessBoard.CoordinateIsValid(coordinate) && AdversaryNearby(coordinate))
                {
                    movePiece[coordinate.Rank, coordinate.File] = true;
                }
                coordinate.GetValue(Coordinate.Rank - 1, Coordinate.File + 1);
                if (ChessBoard.CoordinateIsValid(coordinate) && AdversaryNearby(coordinate))
                {
                    movePiece[coordinate.Rank, coordinate.File] = true;
                }
                // Special Play - En Passant
                if (Coordinate.Rank == 3)
                {
                    Coordinate ToLeft = new Coordinate(Coordinate.Rank, Coordinate.File - 1);
                    if (ChessBoard.CoordinateIsValid(ToLeft) && AdversaryNearby(ToLeft) &&
                        ChessBoard.PieceOnBoard(ToLeft) == ChessMatch.EnPassantVulnerable)
                    {
                        movePiece[ToLeft.Rank - 1, ToLeft.File] = true;
                    }
                    Coordinate ToRight = new Coordinate(Coordinate.Rank, Coordinate.File + 1);
                    if (ChessBoard.CoordinateIsValid(ToRight) && AdversaryNearby(ToRight) &&
                        ChessBoard.PieceOnBoard(ToRight) == ChessMatch.EnPassantVulnerable)
                    {
                        movePiece[ToRight.Rank - 1, ToRight.File] = true;
                    }
                }
            }
            else
            {
                coordinate.GetValue(Coordinate.Rank + 1, Coordinate.File);
                if (ChessBoard.CoordinateIsValid(coordinate) && Empty(coordinate))
                {
                    movePiece[coordinate.Rank, coordinate.File] = true;
                }
                coordinate.GetValue(Coordinate.Rank + 2, Coordinate.File);
                Coordinate MovingToBehind = new Coordinate(Coordinate.Rank + 1, Coordinate.File);
                if (ChessBoard.CoordinateIsValid(MovingToBehind) && Empty(MovingToBehind) &&
                    ChessBoard.CoordinateIsValid(coordinate) && Empty(coordinate) && MovementQuantity == 0)
                {
                    movePiece[coordinate.Rank, coordinate.File] = true;
                }
                coordinate.GetValue(Coordinate.Rank + 1, Coordinate.File - 1);
                if (ChessBoard.CoordinateIsValid(coordinate) && AdversaryNearby(coordinate))
                {
                    movePiece[coordinate.Rank, coordinate.File] = true;
                }
                coordinate.GetValue(Coordinate.Rank + 1, Coordinate.File + 1);
                if (ChessBoard.CoordinateIsValid(coordinate) && AdversaryNearby(coordinate))
                {
                    movePiece[coordinate.Rank, coordinate.File] = true;
                }

                // #En Passant 
                if (Coordinate.Rank == 4)
                {
                    Coordinate ToLeft = new Coordinate(Coordinate.Rank, Coordinate.File - 1);
                    if (ChessBoard.CoordinateIsValid(ToLeft) && AdversaryNearby(ToLeft) &&
                        ChessBoard.PieceOnBoard(ToLeft) == ChessMatch.EnPassantVulnerable)
                    {
                        movePiece[ToLeft.Rank + 1, ToLeft.File] = true;
                    }
                    Coordinate ToRight = new Coordinate(Coordinate.Rank, Coordinate.File + 1);
                    if (ChessBoard.CoordinateIsValid(ToRight) && AdversaryNearby(ToRight) &&
                        ChessBoard.PieceOnBoard(ToRight) == ChessMatch.EnPassantVulnerable)
                    {
                        movePiece[ToRight.Rank + 1, ToRight.File] = true;
                    }
                }
            }
            return movePiece;
        }
    }
}
