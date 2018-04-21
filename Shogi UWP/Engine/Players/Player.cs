using Shogi.Engine.Places;

namespace Shogi.Engine.Players
{
    abstract class PlayerClass
    {
        public Komadai Komadai;
        public PlayerClass(Komadai komadai) => Komadai = komadai;
    }
    enum Player
    {
        Sente,
        Gote
    }
}
