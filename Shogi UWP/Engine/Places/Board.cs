using System.Collections.Generic;

namespace Shogi.Engine.Places
{
    public class Board: Place
    {
        public Field[,] Fields = new Field[9, 9];
        public Field GetField(int row, int column)
        {
            if (row <= 8 && row >= 0 && column <= 8 && column >= 0)
                return Fields[row, column];
            else return null;
        }
        public List<Field> AvailableMoves;

        public Board(Position position) : base(position)
        {
            Position = position;
            for (byte i = 0; i < 9; i++)
                for (byte j = 0; j < 9; j++)
                    Fields[i, j] = new Field(position, this, i, j);
        }
    }  
}
