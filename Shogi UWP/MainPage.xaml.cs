using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Shogi.Engine;
using Shogi.Engine.Pieces;
using Shogi.Engine.Places;

namespace Shogi_UWP
{
    public sealed partial class MainPage : Page
    {
        private Handlers.Piece SelectedPieceHandler;
        public static Handlers.Piece ClickedPieceHandler;
        public MainPage()
        {
            InitializeComponent();
            SizeChanged += MainPage_SizeChanged;
            InitDelegates();
            NewGame();
        }
        private void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ActualWidth >= ActualHeight)
            {
                Shogiban.Width = Shogiban.ActualHeight;
            }
            else
            {
                Shogiban.Width = Double.NaN;
                Shogiban.HorizontalAlignment = HorizontalAlignment.Stretch;
                Shogiban.Height = Shogiban.ActualWidth;
            }
        }
        private void NewGame()
        {
            ShogiEngine.NewShogiGame();
            InitDesk();
        }
        public void InitDelegates()
        {
            ShogiEngine.HideHints += delegate () { Hints.Children.Clear(); };
            ShogiEngine.ShowHints += delegate () {
                foreach (Field field in ShogiEngine.CurrentGame.CurrentPosition.Board.AvailableMoves)
                    Hints.Children.Add(new Handlers.Hint(field));
            };
            ShogiEngine.SelectPieceEvent += SelectPiece;
            ShogiEngine.UnselectPieceEvent += UnselectPiece;
            ShogiEngine.MovePieceEvent += MovePiece;
            ShogiEngine.CreatePieceEvent += CreatePieceVisual;
            ShogiEngine.AddPieceEvent += AddPiece;
            ShogiEngine.RemovePieceEvent += RemovePiece;
        }     
    }
}
