namespace ChessConsole
{
    class BoardMapping
    {
        public char File { get; set; }
        public int Rank { get; set; }
        public BoardMapping(char file, int rank)
        {
            File = file;
            Rank = rank;
        }
        public Coordinate ToCoordinate()
        {
            return new Coordinate(8 - Rank, File - 'a');
        }
        public override string ToString()
        {
            return $"{File}{Rank}";
        }
    }
}
