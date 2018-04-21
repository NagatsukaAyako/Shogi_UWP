using Windows.UI.Xaml.Controls;
using Shogi.Engine;
using Shogi.Engine.Places;
using Shogi.Engine.Pieces;
using Shogi.Engine.Players;

namespace Shogi_UWP
{
    public sealed partial class MainPage
    {
        public void InitDesk()
        {
            //Клетки доски
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    Handlers.Field field = new Handlers.Field(ShogiEngine.CurrentGame.CurrentPosition.Board.GetField(i, j));
                    Fields.Children.Add(field);
                    Grid.SetRow(field, i);
                    Grid.SetColumn(field, j);
                }
        }
        private void MovePiece(Piece piece, Place place)
        {
            Grid.SetRow(piece.Handler as Handlers.Piece, (place as Field).Row);
            Grid.SetColumn(piece.Handler as Handlers.Piece, (place as Field).Column);
        }
        private void AddPiece(Piece piece, Place place)
        {
            if (place is Field)
            {
                Grid.SetRow(piece.Handler as Handlers.Piece, (place as Field).Row);
                Grid.SetColumn(piece.Handler as Handlers.Piece, (place as Field).Column);
                Pieces.Children.Add(piece.Handler as Handlers.Piece);
            }
            else if (place == place.Position.Komadai[Player.Gote])
            {
                GoteKomadai.Children.Add(piece.Handler as Handlers.Piece);
            }
            else SenteKomadai.Children.Add(piece.Handler as Handlers.Piece);
            if (piece.Player == Player.Gote) (piece.Handler as Handlers.Piece).RotateVisual(180);
            else (piece.Handler as Handlers.Piece).RotateVisual(0);
        }

        private void RemovePiece(Piece piece, Place place)
        {
            Handlers.Piece Handler = piece.Handler as Handlers.Piece;
            if (place is Field)
                Pieces.Children.Remove(Handler);
            else if (place == place.Position.Komadai[Player.Gote])
                GoteKomadai.Children.Remove(Handler);
            else SenteKomadai.Children.Remove(Handler);
        }

        private object CreatePieceVisual(Piece piece, Place field)
        {
            return new Handlers.Piece(piece);
        }
        private void SelectPiece(Piece piece, Place place)
        {
            SelectedPieceHandler = piece.Handler as Handlers.Piece;
            (piece.Handler as Handlers.Piece).Background = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(200, 50, 255, 50));
        }
        private void UnselectPiece(Piece piece, Place place)
        {
            (piece.Handler as Handlers.Piece).Background = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Transparent);
        }
    }
}
