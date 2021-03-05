namespace ChessConsole
{
    abstract class ChessPiece
    {
        public Coordinate Coordinate { get; set; }
        public Coloring Coloring { get; protected set; }
        public int MovementQuantity { get; protected set; }
        public ChessBoard ChessBoard { get; protected set; }
        public ChessPiece(ChessBoard chessBoard, Coloring coloring)
        {
            Coordinate = null;
            Coloring = coloring;
            MovementQuantity = 0;
            ChessBoard = chessBoard;          
        }
        public void MovementIncrement()
        {
            MovementQuantity++;
        }
        public void MovementDecrement()
        {
            MovementQuantity--;
        }
        public abstract bool[,] PieceMoveSet();

        public bool MovementAvaliableList()
        {
            bool[,] movementAvaliable = PieceMoveSet();
            for (int i = 0; i < ChessBoard.Ranks; i++)
            {
                for (int j = 0; j < ChessBoard.Files; j++)
                {
                    if (movementAvaliable[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool MovementIsAvaliable(Coordinate coordinate)
        {
            return PieceMoveSet()[coordinate.Rank, coordinate.File];
        }
    }
}
