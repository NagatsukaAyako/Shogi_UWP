using System.Collections.Generic;
using Shogi.Engine.Places;
using Shogi.Engine.Players;
using Shogi.Engine.Pieces;
namespace Shogi.Engine
{
    public class Position
    {
        public Game Game;
        public Board Board;
        public Dictionary<Player, Komadai> Komadai = new Dictionary<Player, Komadai>();
        public Piece SelectedPiece { get; set; }
        public Position(Game game)
        {
            Game = game;
            Board = new Board(this);
            Komadai.Add(Player.Sente, new Komadai(this));
            Komadai.Add(Player.Gote, new Komadai(this));
        }
    }
}
