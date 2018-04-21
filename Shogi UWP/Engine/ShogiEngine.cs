using System.Collections.Generic;
using Shogi.Engine.Pieces;
using Shogi.Engine.Places;
using Shogi.Engine.Players;

namespace Shogi.Engine
{
    public static class ShogiEngine
    {
        public delegate void Operation();
        public delegate void ShogiDelegate(Piece piece, Place place);
        public delegate object HandlerShogiDelegate(Piece piece, Place place);
        public delegate void PPShogiDelegate(Piece piece, Piece piece2);
        //public static event ShogiOperation ShowLastMove;
        public static event Operation ShowHints;
        public static event Operation HideHints;
        public static event HandlerShogiDelegate CreatePieceEvent;
        public static event ShogiDelegate AddPieceEvent;
        public static event ShogiDelegate RemovePieceEvent;
        public static event ShogiDelegate SelectPieceEvent;
        public static event ShogiDelegate UnselectPieceEvent;
        public static event ShogiDelegate TransferTurnEvent;
        public static event ShogiDelegate MovePieceEvent;

        public static Game CurrentGame;        
        public static void NewShogiGame()
        {
            CurrentGame = new Game();
        }
        private static void TransferTurn(Game game)
        {
            game.CurrentTurn = game.CurrentTurn == Player.Sente ? Player.Gote : Player.Sente;
            TransferTurnEvent?.Invoke(null, null);
        } 
        public static void TrySelectPiece(Piece piece)
        {
            if (piece.Player == piece.Position.Game.CurrentTurn)
                SelectOwnPiece(piece);
            else if (piece.Position.SelectedPiece != null)
                if (piece.Position.Board.AvailableMoves.Contains(piece.Location as Field))
                    MovePiece(piece.Position.SelectedPiece, piece.Location);
                else UnselectPiece(piece.Position.SelectedPiece);
        }
        public static void TrySelectField(Field field)
        {
            if (field.Position.SelectedPiece != null)
                if (field.Board.AvailableMoves.Contains(field))
                    if (field.Position.SelectedPiece.Location is Field)
                        MovePiece(field.Position.SelectedPiece, field);
                    else ThrowPiece(field.Position.SelectedPiece, field);
                else UnselectPiece(field.Position.SelectedPiece);
        }
        private static void SelectOwnPiece(Piece piece)
        {
            if (piece.Position.SelectedPiece == null)
                SelectPiece(piece);
            else if (piece == piece.Position.SelectedPiece)
                UnselectPiece(piece);
            else
            {
                UnselectPiece(piece.Position.SelectedPiece);
                SelectPiece(piece);
            }
        }
        private static void MovePiece(Piece piece, Place place)
        {
            HideHints();
            Field field = place as Field;
            (field.Position.SelectedPiece.Location as Field).Piece = null;
            field.Position.SelectedPiece.Location = field;
            MovePieceEvent(piece,place);
            //действия с битой фигурой
            if (field.Piece != null) 
            {
                Piece beatPiece = field.Piece;
                RemovePieceEvent(field.Piece, field);
                beatPiece.Player = beatPiece.Player == Player.Sente ? Player.Gote : Player.Sente;
                if (beatPiece is IPromoted)
                    CreatePiece((beatPiece as IPromoted).DefaultPiece(), beatPiece.Position.Komadai[beatPiece.Player], beatPiece.Player);
                else 
                    AddPiece(beatPiece, beatPiece.Position.Komadai[beatPiece.Player]);
            }
            field.Piece = field.Position.SelectedPiece;
            //превращение фигуры
            if (field.Position.SelectedPiece is IUnpromoted && (field.Position.SelectedPiece.Player == Player.Sente ? field.Row <= 2 : field.Row >= 6))
                TransformPiece(field.Position.SelectedPiece);
            UnselectPiece(field.Position.SelectedPiece);
            TransferTurn(field.Position.Game);
        }
        private static void TransformPiece(Piece piece)
        {
            Piece TransformedPiece = (piece as IUnpromoted).TransformedPiece();
            RemovePiece(piece);
            CreatePiece(TransformedPiece, piece.Location, piece.Player);
            piece.Location.Position.SelectedPiece = TransformedPiece;
        }
        private static void ThrowPiece(Piece piece, Field field)
        {
            HideHints();
            RemovePiece(piece);
            AddPiece(field.Position.SelectedPiece, field);
            UnselectPiece(field.Position.SelectedPiece);
            TransferTurn(field.Position.Game);
        }
        private static void CreatePiece(Piece piece, Place place, Player player) 
        {
            piece.Handler = CreatePieceEvent(piece, place);
            piece.Player = player;
            AddPiece(piece, place);
        }
        private static void AddPiece(Piece piece, Place place)
        {
            if(place is Field)
                (place as Field).Piece = piece;
            if (place is Komadai)
                (place as Komadai).Pieces.Add(piece);
            piece.Location = place;
            AddPieceEvent(piece, place);
        }
        private static void RemovePiece(Piece piece)
        {
            Place place = piece.Location;
            if (piece.Location is Field)
                (piece.Location as Field).Piece = piece;
            if (piece.Location is Komadai)
                (piece.Location as Komadai).Pieces.Remove(piece);
            RemovePieceEvent(piece, place);
        }
        private static void SelectPiece(Piece piece)
        {
            piece.Position.SelectedPiece = piece;
            if (piece.Location is Komadai)
                piece.Position.Board.AvailableMoves = GetAvalaibleToThrow(piece);
            else
                piece.Position.Board.AvailableMoves = piece.GetAvailablesMoves();
            ShowHints();
            SelectPieceEvent(piece, piece.Location);
        }
        private static void UnselectPiece(Piece piece)
        {
            piece.Position.SelectedPiece = null;
            HideHints();
            UnselectPieceEvent(piece, piece.Location);
        }
        private static List<Field> GetAvalaibleToThrow(Piece piece) {
            List<Field> fields = new List<Field>();
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (piece.Position.Board.Fields[i, j].Piece == null)
                        fields.Add(piece.Position.Board.Fields[i, j]);
            return fields;
        }
        
        public static void SetStartPosition(Board board)
        {
            for (int i = 0; i < 9; i++)
            {
                CreatePiece(new Pawn(board.Position), board.Fields[2, i], Player.Gote);
                CreatePiece(new Pawn(board.Position),  board.Fields[6, i], Player.Sente);
            }
            CreatePiece(new King(board.Position), board.Fields[0, 4], Player.Gote);
            CreatePiece(new King(board.Position), board.Fields[8, 4] , Player.Sente);
            CreatePiece(new Gold(board.Position), board.Fields[8, 3] , Player.Sente);
            CreatePiece(new Gold(board.Position), board.Fields[0, 3] , Player.Gote);
            CreatePiece(new Gold(board.Position), board.Fields[8, 5], Player.Sente);
            CreatePiece(new Gold(board.Position), board.Fields[0, 5], Player.Gote);
            CreatePiece(new Silver(board.Position), board.Fields[8, 6], Player.Sente);
            CreatePiece(new Silver(board.Position), board.Fields[0, 6], Player.Gote);
            CreatePiece(new Silver(board.Position), board.Fields[8, 2], Player.Sente);
            CreatePiece(new Silver(board.Position), board.Fields[0, 2], Player.Gote);
            CreatePiece(new Knight(board.Position), board.Fields[8, 7], Player.Sente);
            CreatePiece(new Knight(board.Position), board.Fields[0, 7], Player.Gote);
            CreatePiece(new Knight(board.Position), board.Fields[8, 1], Player.Sente);
            CreatePiece(new Knight(board.Position), board.Fields[0, 1] , Player.Gote);
            CreatePiece(new Lance(board.Position), board.Fields[8, 8] , Player.Sente);
            CreatePiece(new Lance(board.Position), board.Fields[0, 8], Player.Gote);
            CreatePiece(new Lance(board.Position), board.Fields[8, 0], Player.Sente);
            CreatePiece(new Lance(board.Position), board.Fields[0, 0], Player.Gote);
            CreatePiece(new Rook(board.Position), board.Fields[7, 7], Player.Sente);
            CreatePiece(new Rook(board.Position), board.Fields[1, 1] , Player.Gote);
            CreatePiece(new Beashop(board.Position), board.Fields[7, 1], Player.Sente);
            CreatePiece(new Beashop(board.Position), board.Fields[1, 7], Player.Gote);
        }
    }
}
