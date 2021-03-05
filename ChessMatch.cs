using System.Collections.Generic;

namespace ChessConsole
{
    class ChessMatch
    {
        public ChessBoard ChessBoard { get; private set; }
        public int Shift { get; private set; }
        public Coloring CurrentPlayer { get; private set; }
        public bool End { get; private set; }
        private HashSet<ChessPiece> PiecesInGame;
        private HashSet<ChessPiece> PiecesCaptured;
        public bool Check { get; private set; }
        public ChessPiece EnPassantVulnerable { get; private set; }
        public ChessMatch()
        {
            ChessBoard = new ChessBoard(8, 8);
            Shift = 1;
            CurrentPlayer = Coloring.White;
            End = false;
            Check = false;
            EnPassantVulnerable = null;
            PiecesInGame = new HashSet<ChessPiece>();
            PiecesCaptured = new HashSet<ChessPiece>();
            StandardPiecesCoordinate();
        }
        public ChessPiece Movement(Coordinate moveFrom, Coordinate moveTo)
        {
            ChessPiece movedPiece = ChessBoard.RemovePiece(moveFrom);
            movedPiece.MovementIncrement();
            ChessPiece pieceCaptured = ChessBoard.RemovePiece(moveTo);
            ChessBoard.AddPiece(movedPiece, moveTo);
            if (pieceCaptured != null)
            {
                PiecesCaptured.Add(pieceCaptured);
            }
            // Special Play [Minor Castling]
            if (movedPiece is King && moveTo.File == moveTo.File + 2)
            {
                Coordinate RookInitialCordinate = new Coordinate(moveTo.Rank, moveTo.File + 3);
                Coordinate RookAfterMoveCoordinate = new Coordinate(moveTo.Rank, moveTo.File + 1);
                ChessPiece Rook = ChessBoard.RemovePiece(RookInitialCordinate);
                Rook.MovementIncrement();
                ChessBoard.AddPiece(Rook, RookAfterMoveCoordinate);
            }
            // Special Play [Great Castling]
            if (movedPiece is King && moveTo.File == moveTo.File - 2)
            {
                Coordinate RookInitialCordinate = new Coordinate(moveTo.Rank, moveTo.File - 4);
                Coordinate RookAfterMoveCoordinate = new Coordinate(moveTo.Rank, moveTo.File - 1);
                ChessPiece Rook = ChessBoard.RemovePiece(RookInitialCordinate);
                Rook.MovementIncrement();
                ChessBoard.AddPiece(Rook, RookAfterMoveCoordinate);
            }
            // Special Play En Passant
            if (movedPiece is Pawn)
            {
                if (moveFrom.File != moveTo.File && pieceCaptured == null)
                {
                    Coordinate PawnCordinate;
                    if (movedPiece.Coloring == Coloring.White)
                    {
                        PawnCordinate = new Coordinate(moveTo.Rank + 1, moveTo.File);
                    }
                    else
                    {
                        PawnCordinate = new Coordinate(moveTo.Rank - 1, moveTo.File);
                    }
                    pieceCaptured = ChessBoard.RemovePiece(PawnCordinate);
                    PiecesCaptured.Add(pieceCaptured);
                }
            }
            return pieceCaptured;
        }     
        public void UndoMovement(Coordinate moveFrom, Coordinate moveTo, ChessPiece capturedPiece)
        {
            ChessPiece piece = ChessBoard.RemovePiece(moveTo);
            piece.MovementDecrement();
            if (capturedPiece != null)
            {
                ChessBoard.AddPiece(capturedPiece, moveTo);
                PiecesCaptured.Remove(capturedPiece);
            }
            ChessBoard.AddPiece(piece, moveFrom);
            // #Special Play - Minor Castling
            if (piece is King && moveTo.File == moveFrom.File + 2)
            {
                Coordinate RookInitialCoordinate = new Coordinate(moveFrom.Rank, moveFrom.File + 3);
                Coordinate RookAfterMoveCoordinate = new Coordinate(moveFrom.Rank, moveFrom.File + 1);
                ChessPiece Rook = ChessBoard.RemovePiece(RookAfterMoveCoordinate);
                Rook.MovementDecrement();
                ChessBoard.AddPiece(Rook, RookInitialCoordinate);
            }

            // #Special Play - Great Castling
            if (piece is King && moveTo.File == moveFrom.File - 2)
            {
                Coordinate RookInitialCoordinate = new Coordinate(moveFrom.Rank, moveFrom.File - 4);
                Coordinate RookAfterMoveCoordinate = new Coordinate(moveFrom.Rank, moveFrom.File - 1);
                ChessPiece Rook = ChessBoard.RemovePiece(RookAfterMoveCoordinate);
                Rook.MovementDecrement();
                ChessBoard.AddPiece(Rook, RookInitialCoordinate);
            }

            // #Special Play - En Passant
            if (piece is Pawn)
            {
                if (moveFrom.File != moveTo.File && capturedPiece == EnPassantVulnerable)
                {
                    ChessPiece pawn = ChessBoard.RemovePiece(moveTo);
                    Coordinate PawnCoordinate;
                    if (piece.Coloring == Coloring.White)
                    {
                        PawnCoordinate = new Coordinate(3, moveTo.File);
                    }
                    else
                    {
                        PawnCoordinate = new Coordinate(4, moveTo.File);
                    }
                    ChessBoard.AddPiece(pawn, PawnCoordinate);
                }
            }
        }
        public void ShiftManager(Coordinate moveFrom, Coordinate moveTo)
        {
            ChessPiece capturedPiece = Movement(moveFrom, moveTo);
            if (InCheck(CurrentPlayer))
            {
                UndoMovement(moveFrom, moveTo, capturedPiece);
                throw new ChessConsoleException("You can't put yourself in Check!");
            }
            ChessPiece piece = ChessBoard.PieceOnBoard(moveTo);

            // Special Play Promote
            if (piece is Pawn)
            {
                if ((piece.Coloring == Coloring.White && moveTo.Rank == 0) ||
                    (piece.Coloring == Coloring.Black && moveTo.Rank == 7))
                {
                    piece = ChessBoard.RemovePiece(moveTo);
                    PiecesInGame.Remove(piece);
                    ChessPiece queen = new Queen(ChessBoard, piece.Coloring);
                    ChessBoard.AddPiece(queen, moveTo);
                    PiecesInGame.Add(queen);
                }
            }
            if (InCheck(Adversary(CurrentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            if (CheckMate(Adversary(CurrentPlayer)))
            {
                End = true;
            }
            else
            {
                Shift++;
                ChangeCurrentPlayer();
            }

            // #Special Play En Passant
            if (piece is Pawn && (moveTo.Rank == moveFrom.Rank - 2 || moveTo.Rank == moveFrom.Rank + 2))
            {
                EnPassantVulnerable = piece;
            }
            else
            {
                EnPassantVulnerable = null;
            }
        }
        public void IsValidMoveFrom(Coordinate coordinate)
        {
            if (ChessBoard.PieceOnBoard(coordinate) == null)
            {
                throw new ChessConsoleException("There's no piece to move!");
            }
            if (CurrentPlayer != ChessBoard.PieceOnBoard(coordinate).Coloring)
            {
                throw new ChessConsoleException("That piece is not yours!");
            }
            if (!ChessBoard.PieceOnBoard(coordinate).MovementAvaliableList())
            {
                throw new ChessConsoleException("Don't exist avaliable movements for that piece!");
            }
        }
        public void IsValidMoveTo(Coordinate moveFrom, Coordinate moveTo)
        {
            if (!ChessBoard.PieceOnBoard(moveFrom).MovementIsAvaliable(moveTo))
            {
                throw new ChessConsoleException("Coordinate to move is invalid!");
            }
        }
        private void ChangeCurrentPlayer()
        {
            if (CurrentPlayer == Coloring.White)
            {
                CurrentPlayer = Coloring.Black;
            }
            else
            {
                CurrentPlayer = Coloring.White;
            }
        }
        public HashSet<ChessPiece> PieceCapturedByTeam(Coloring coloring)
        {
            HashSet<ChessPiece> pieceCaptured = new HashSet<ChessPiece>();
            foreach (ChessPiece chessPiece in PiecesCaptured)
            {
                if (chessPiece.Coloring == coloring)
                {
                    pieceCaptured.Add(chessPiece);
                }
            }
            return pieceCaptured;
        }
        public HashSet<ChessPiece> PieceInGameByTeam(Coloring coloring)
        {
            HashSet<ChessPiece> pieceInGame = new HashSet<ChessPiece>();
            foreach (ChessPiece chessPiece in PiecesInGame)
            {
                if (chessPiece.Coloring == coloring)
                {
                    pieceInGame.Add(chessPiece);
                }
            }
            pieceInGame.ExceptWith(PieceCapturedByTeam(coloring));
            return pieceInGame;
        }
        private Coloring Adversary(Coloring coloring)
        {
            if (coloring == Coloring.White)
            {
                return Coloring.Black;
            }
            else
            {
                return Coloring.White;
            }
        }
        private ChessPiece King(Coloring coloring)
        {
            foreach (ChessPiece chessPiece in PieceInGameByTeam(coloring))
            {
                if (chessPiece is King)
                {
                    return chessPiece;
                }
            }
            return null;
        }
        public bool InCheck(Coloring coloring)
        {
            ChessPiece king = King(coloring);
            if (king == null)
            {
                throw new ChessConsoleException($"There is no {coloring} King in the match!");
            }
            foreach (ChessPiece chessPiece in PieceInGameByTeam(Adversary(coloring)))
            {
                bool[,] piece = chessPiece.PieceMoveSet();
                if (piece[king.Coordinate.Rank, king.Coordinate.File])
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckMate(Coloring coloring)
        {
            if (!InCheck(coloring))
            {
                return false;
            }
            foreach (ChessPiece chessPiece in PieceInGameByTeam(coloring))
            {
                bool[,] mat = chessPiece.PieceMoveSet();
                for (int i = 0; i < ChessBoard.Ranks; i++)
                {
                    for (int j = 0; j < ChessBoard.Files; j++)
                    {
                        if (mat[i, j])
                        {
                            Coordinate moveFrom = chessPiece.Coordinate;
                            Coordinate moveTo = new Coordinate(i, j);
                            ChessPiece pieceCaptured = Movement(moveFrom, moveTo);
                            bool inCheck = InCheck(coloring);
                            UndoMovement(moveFrom, moveTo, pieceCaptured);
                            if (!inCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        public void NewPiece(char file, int rank, ChessPiece chessPiece)
        {
            ChessBoard.AddPiece(chessPiece, new BoardMapping(file, rank).ToCoordinate());
            PiecesInGame.Add(chessPiece);
        }
        private void StandardPiecesCoordinate()
        {
            NewPiece('a', 1, new Rook(ChessBoard, Coloring.White));
            NewPiece('b', 1, new Knight(ChessBoard, Coloring.White));
            NewPiece('c', 1, new Bishop(ChessBoard, Coloring.White));
            NewPiece('d', 1, new Queen(ChessBoard, Coloring.White));
            NewPiece('e', 1, new King(ChessBoard, Coloring.White, this));
            NewPiece('f', 1, new Bishop(ChessBoard, Coloring.White));
            NewPiece('g', 1, new Knight(ChessBoard, Coloring.White));
            NewPiece('h', 1, new Rook(ChessBoard, Coloring.White));
            NewPiece('a', 2, new Pawn(ChessBoard, Coloring.White, this));
            NewPiece('b', 2, new Pawn(ChessBoard, Coloring.White, this));
            NewPiece('c', 2, new Pawn(ChessBoard, Coloring.White, this));
            NewPiece('d', 2, new Pawn(ChessBoard, Coloring.White, this));
            NewPiece('e', 2, new Pawn(ChessBoard, Coloring.White, this));
            NewPiece('f', 2, new Pawn(ChessBoard, Coloring.White, this));
            NewPiece('g', 2, new Pawn(ChessBoard, Coloring.White, this));
            NewPiece('h', 2, new Pawn(ChessBoard, Coloring.White, this));

            NewPiece('a', 8, new Rook(ChessBoard, Coloring.Black));
            NewPiece('b', 8, new Knight(ChessBoard, Coloring.Black));
            NewPiece('c', 8, new Bishop(ChessBoard, Coloring.Black));
            NewPiece('d', 8, new Queen(ChessBoard, Coloring.Black));
            NewPiece('e', 8, new King(ChessBoard, Coloring.Black, this));
            NewPiece('f', 8, new Bishop(ChessBoard, Coloring.Black));
            NewPiece('g', 8, new Knight(ChessBoard, Coloring.Black));
            NewPiece('h', 8, new Rook(ChessBoard, Coloring.Black));
            NewPiece('a', 7, new Pawn(ChessBoard, Coloring.Black, this));
            NewPiece('b', 7, new Pawn(ChessBoard, Coloring.Black, this));
            NewPiece('c', 7, new Pawn(ChessBoard, Coloring.Black, this));
            NewPiece('d', 7, new Pawn(ChessBoard, Coloring.Black, this));
            NewPiece('e', 7, new Pawn(ChessBoard, Coloring.Black, this));
            NewPiece('f', 7, new Pawn(ChessBoard, Coloring.Black, this));
            NewPiece('g', 7, new Pawn(ChessBoard, Coloring.Black, this));
            NewPiece('h', 7, new Pawn(ChessBoard, Coloring.Black, this));
        }
    }
}
