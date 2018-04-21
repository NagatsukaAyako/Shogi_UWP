using Shogi.Engine.Pieces;
namespace Shogi.Engine.Places
{
    class Field : Place
    {
        public readonly byte Row;
        public readonly byte Column;
        public readonly Board Board;
        public Piece Piece;
        public Field(Position position, Board board, byte row, byte column) : base(position)
        {
            Row = row;
            Column = column;
            Board = board;
        }
    }
}
