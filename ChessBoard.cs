namespace ChessConsole112376152376521637
{
    class ChessBoard
    {
        public int Ranks { get; set; }
        public int Files { get; set; }
        private ChessPiece[,] ChessPieces;
        public ChessBoard(int ranks, int files)
        {
            Ranks = ranks;
            Files = files;
            ChessPieces = new ChessPiece[ranks, files];
        }
        public ChessPiece PieceOnBoard(int rank, int file)
        {
            return ChessPieces[rank, file];
        }
        public ChessPiece PieceOnBoard(Coordinate coordinate)
        {
            return ChessPieces[coordinate.Rank, coordinate.File];
        }
        
        public void AddPiece(ChessPiece chessPiece, Coordinate coordinate)
        {
            if (AlreadyExistsAPiece(coordinate))
            {
                throw new ChessConsoleException("Already exists a piece in that coordinate!");
            }
            else
            {
                ChessPieces[coordinate.Rank, coordinate.File] = chessPiece;
                chessPiece.Coordinate = coordinate;
            }
        }
        public ChessPiece RemovePiece(Coordinate coordinate)
        {
            if (PieceOnBoard(coordinate) == null)
            {
                return null;
            }
            else
            {
                ChessPiece chessPiece = PieceOnBoard(coordinate);
                chessPiece.Coordinate = null;
                ChessPieces[coordinate.Rank, coordinate.File] = null;
                return chessPiece;
            }
        }
        public bool AlreadyExistsAPiece(Coordinate coordinate)
        {
            CoordinateIsValidException(coordinate);
            return PieceOnBoard(coordinate) != null;
        }
        public bool CoordinateIsValid(Coordinate coordinate)
        {
            if (coordinate.Rank < 0 || coordinate.Rank >= Ranks ||
                coordinate.File < 0 || coordinate.File >= Files)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void CoordinateIsValidException(Coordinate coordinate)
        {
            if (!CoordinateIsValid(coordinate))
            {
                throw new ChessConsoleException("Invalid Coordinate!");
            }
        }
    }
}
