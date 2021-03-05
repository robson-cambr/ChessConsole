namespace ChessConsole
{
    class Coordinate
    {
        public int Rank { get; set; }
        public int File { get; set; }
        public Coordinate(int rank, int file)
        {
            Rank = rank;
            File = file;
        }
        public void GetValue(int rank, int file)
        {
            Rank = rank;
            File = file;
        }
        public override string ToString()
        {
            return $"{Rank}, {File}";
        }
    }
}
