using Shogi.Engine.Players;

namespace Shogi.Engine
{
    public class Game
    {
        public Player CurrentTurn;
        public Position CurrentPosition;
        public Game()
        {
            CurrentPosition = new Position(this);
            CurrentTurn = Player.Sente;
            ShogiEngine.SetStartPosition(CurrentPosition.Board);
        }
    }
}
