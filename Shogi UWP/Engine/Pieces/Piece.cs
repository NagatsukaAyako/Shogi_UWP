using System.Collections.Generic;
using Shogi.Engine.Places;
using Shogi.Engine.Players;

namespace Shogi.Engine.Pieces
{
    public abstract class Piece
    {
        public object Handler;
        public abstract string Label { get; }
        public readonly Position Position;
        public Place Location;
        public Player Player;
        public Piece(Position position) => Position = position;

        public abstract List<Field> GetAvailablesMoves();
        protected List<Field> CalculateAvailable(Field field, List<(int, int)> Listparams = null, params (int, int)[] fieldparams)
        {
            List<Field> moveList = new List<Field>();
            if (Listparams != null)
                foreach ((int, int) i in Listparams)
                    moveList.AddRange(Line(i.Item1, i.Item2));
            foreach ((int, int) i in fieldparams)
                AddField(moveList, GetFieldMove(field, i.Item1, i.Item2));
            return moveList;
            Field GetFieldMove(Field fieldLocal, int r, int c)
            {
                Field f = (fieldLocal.Board).GetField(fieldLocal.Row + r, fieldLocal.Column + c);
                if (f?.Piece?.Player != f?.Position?.Game?.CurrentTurn)
                    return f;
                else return null;
            }
            List<Field> Line(int drow, int dcol)
            {
                List<Field> list = new List<Field>();
                Field fieldIter = field;
                while (true)
                {
                    fieldIter = GetFieldMove(fieldIter, drow, dcol);
                    if (fieldIter == null) break; // моя фигура или край доски
                    list.Add(fieldIter);//пустая клетка или чужая фигура
                    if (fieldIter.Piece != null) break;
                }
                return list;
            }
            void AddField(List<Field> list, Field fieldLocal)
            {
                if (fieldLocal != null)
                    list.Add(fieldLocal);
            }
        }
    }
}
