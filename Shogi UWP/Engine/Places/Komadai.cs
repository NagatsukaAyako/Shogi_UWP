using System.Collections.Generic;
using Shogi.Engine.Pieces;

namespace Shogi.Engine.Places
{
    public class Komadai : Place
    {
        public List<Piece> Pieces = new List<Piece>();
        public Komadai(Position position) : base(position) { }
    }
}
