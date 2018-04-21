using System.Collections.Generic;
using Shogi.Engine.Places;

namespace Shogi.Engine.Pieces
{     
    interface IUnpromoted
    {
        Piece TransformedPiece();
    }
    interface IPromoted
    {
        Piece DefaultPiece();
    }

    class King : Piece
    {
        public override string Label { get{ return "王"; } }
        public King(Position position) : base(position) { }
        public override List<Field> GetAvailablesMoves()
        {
            int Direction = Player == Players.Player.Sente ? -1 : 1;
            return CalculateAvailable(Location as Field, null,
                (Direction, 0),
                (-Direction, 0),
                ( 0, Direction),
                ( 0, -Direction),
                (Direction, Direction),
                ( Direction, -Direction),
                ( -Direction, Direction),
                (-Direction, -Direction)
            );
        }
    }
    class Rook : Piece, IUnpromoted
    {
        public override string Label { get { return "飛"; } }
        public Piece TransformedPiece() { return new Dragon(Position); }
        public Rook(Position position) : base(position) { }
        public override List<Field> GetAvailablesMoves()
        {
            int Direction = Player == Players.Player.Sente ? -1 : 1;
            return CalculateAvailable(Location as Field, 
                new List<(int, int)>(){
                    (Direction, 0),
                    (-Direction, 0),
                    (0, Direction),
                    (0, -Direction)
                }
            );
        }
    }
    class Beashop : Piece, IUnpromoted
    {
        public override string Label { get { return "角"; } }
        public Beashop(Position position) : base(position) { }
        public Piece TransformedPiece() { return new Horse(Position); }
        public override List<Field> GetAvailablesMoves()
        {
            int Direction = Player == Players.Player.Sente ? -1 : 1;
            return CalculateAvailable(Location as Field,
                new List<(int, int)>(){
                    (Direction, Direction),
                    (Direction, -Direction),
                    (-Direction, Direction),
                    (-Direction, -Direction)
                }
            );
        }
    }
    class Gold : Piece
    {
        public override string Label { get { return "金"; } }
        public Gold(Position position) : base(position) { }
        public override List<Field> GetAvailablesMoves()
        {
            int Direction = Player == Players.Player.Sente ? -1 : 1;
            return CalculateAvailable(Location as Field, null,
                (Direction, 0),
                (-Direction, 0),
                (0, Direction),
                (0, -Direction),
                (Direction, Direction),
                (Direction, -Direction)
            );
        }
    }
    class Silver : Piece, IUnpromoted
    {
        public override string Label { get { return "銀"; } }
        public Piece TransformedPiece() { return new PromotedSilver(Position); }
        public Silver(Position position) : base(position) { }
        public override List<Field> GetAvailablesMoves()
        {
            int Direction = Player == Players.Player.Sente ? -1 : 1;
            return CalculateAvailable(Location as Field, null,
                (Direction, 0),
                (Direction, Direction),
                (Direction, -Direction),
                 (-Direction, Direction),
                (-Direction, -Direction)
            );
        }
    }
    class Knight : Piece, IUnpromoted
    {
        public override string Label { get { return "桂"; } }
        public Piece TransformedPiece() { return new PromotedKnight(Position); }
        public Knight(Position position) : base(position) { }
        public override List<Field> GetAvailablesMoves()
        {
            int Direction = Player == Players.Player.Sente ? -1 : 1;
            return CalculateAvailable(Location as Field, null,
                (2 * Direction, -Direction),
                (2 * Direction, Direction)
            );
        }
    }
    class Lance : Piece, IUnpromoted
    {
        public override string Label { get { return "香"; } }
        public Piece TransformedPiece() { return new PromotedLance(Position); }
        public Lance(Position position) : base(position) { }
        public override List<Field> GetAvailablesMoves()
        {
            int Direction = Player == Players.Player.Sente ? -1 : 1;
            return CalculateAvailable(Location as Field, new List<(int, int)>(){ (Direction, 0) });
        }
    }      
    class Pawn : Piece, IUnpromoted
    {
        public override string Label { get { return "歩"; } }
        public Piece TransformedPiece() { return new Tokin(Position); }
        public Pawn(Position position) : base(position) { }
        public override List<Field> GetAvailablesMoves()
        {
            int Direction = Player == Players.Player.Sente ? -1 : 1;
            return CalculateAvailable(Location as Field, null,
                (Direction, 0)
            );
        }
    }
    class Dragon : Piece, IPromoted
    {
        public override string Label { get { return "竜"; } }
        public Piece DefaultPiece() { return new Rook(Position); }
        public Dragon(Position position) : base(position) { }
        public override List<Field> GetAvailablesMoves()
        {
            int Direction = Player == Players.Player.Sente ? -1 : 1;
            return CalculateAvailable(Location as Field,
                new List<(int, int)>(){
                    (Direction, 0),
                    (-Direction, 0),
                    (0, Direction),
                    (0, -Direction)
                },
                (Direction, Direction),
                (Direction, -Direction),
                (-Direction, Direction),
                (-Direction, -Direction)
            );
        }
    }
    class Horse : Piece, IPromoted
    {
        public override string Label { get { return "馬"; } }
        public Piece DefaultPiece() { return new Beashop(Position); }
        public Horse(Position position) : base(position) { }
        public override List<Field> GetAvailablesMoves()
        {
            int Direction = Player == Players.Player.Sente ? -1 : 1;
            return CalculateAvailable(Location as Field,
                new List<(int, int)>(){
                    (Direction, Direction),
                    (Direction, -Direction),
                    (-Direction, Direction),
                    (-Direction, -Direction)
                },
                (Direction, 0),
                (-Direction, 0),
                (0, Direction),
                (0, -Direction)
            );
        }
    }
    class PromotedSilver : Piece, IPromoted
    {
        public override string Label { get { return "全"; } }
        public Piece DefaultPiece() { return new Silver(Position); }
        public PromotedSilver(Position position) : base(position) { }
        public override List<Field> GetAvailablesMoves()
        {
            int Direction = Player == Players.Player.Sente ? -1 : 1;
            return CalculateAvailable(Location as Field, null,
                (Direction, 0),
                (-Direction, 0),
                (0, Direction),
                (0, -Direction),
                (Direction, Direction),
                (Direction, -Direction)
            );
        }
    }
    class PromotedLance : Piece, IPromoted
    {
        public override string Label { get { return "杏"; } }
        public Piece DefaultPiece() {  return new Lance(Position); }
        public PromotedLance(Position position) : base(position) { }
        public override List<Field> GetAvailablesMoves()
        {
            int Direction = Player == Players.Player.Sente ? -1 : 1;
            return CalculateAvailable(Location as Field, null,
                (Direction, 0),
                (-Direction, 0),
                (0, Direction),
                (0, -Direction),
                (Direction, Direction),
                (Direction, -Direction)
            );
        }
    }
    class PromotedKnight : Piece, IPromoted
    {
        public override string Label { get { return "圭"; } }
        public Piece DefaultPiece() { return new Knight(Position); }
        public PromotedKnight(Position position) : base(position) { }
        public override List<Field> GetAvailablesMoves()
        {
            int Direction = Player == Players.Player.Sente ? -1 : 1;
            return CalculateAvailable(Location as Field, null,
                (Direction, 0),
                (-Direction, 0),
                (0, Direction),
                (0, -Direction),
                (Direction, Direction),
                (Direction, -Direction)
            );
        }
    }
    class Tokin : Piece, IPromoted
    {
        public override string Label { get { return "と"; } }
        public Piece DefaultPiece(){ return new Pawn(Position); }
        public Tokin(Position position) : base(position) { }
        public override List<Field> GetAvailablesMoves()
        {
            int Direction = Player == Players.Player.Sente ? -1 : 1;
            return CalculateAvailable(Location as Field, null,
                (Direction, 0),
                (-Direction, 0),
                (0, Direction),
                (0, -Direction),
                (Direction, Direction),
                (Direction, -Direction)
            );
        }
    }
}
