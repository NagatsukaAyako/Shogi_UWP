using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.Foundation;
using Shogi.Engine;
using Shogi.Engine.Pieces;

namespace Shogi_UWP.Handlers
{
    public class Piece : Grid
    {
        public Shogi.Engine.Pieces.Piece PieceEngine { get; }
        public Piece(Shogi.Engine.Pieces.Piece piece)
        {
            PieceEngine = piece;
            PointerPressed += delegate (object sender, PointerRoutedEventArgs e) {
                MainPage.ClickedPieceHandler = this;
                ShogiEngine.TrySelectPiece(PieceEngine);
            };
            Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
            switch (PieceEngine)
            {
                case King king: Padding = new Thickness(0); break;
                case Rook rook: case Dragon dragon: case Beashop beashop: case Horse horse: Padding = new Thickness(1); break;
                case Gold gold: case Silver silver: case PromotedSilver ps: Padding = new Thickness(2); break;
                case Knight knight: case PromotedKnight pk: Padding = new Thickness(3); break;
                case Lance knight: case PromotedLance pl: Padding = new Thickness(4); break;
                case Pawn pawn: case Tokin tokin: Padding = new Thickness(5); break;
            }
            BorderThickness = new Thickness(2);
            Children.Add(new Windows.UI.Xaml.Shapes.Polygon
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Stretch,
                Stretch = Stretch.Uniform,
                Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 222, 173)),
                Points = new PointCollection {
                        new Point(1.45, 0), new Point(2.7, 0.4),
                            new Point(2.9, 3.2), new Point(0, 3.2), new Point(0.2, 0.4)
                    }
            });
            Children.Add(new TextBlock
            {
                Text = PieceEngine.Label,
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            });
            RenderTransformOrigin = new Point(0.5, 0.5);

        }
        public void RotateVisual(int angle)
        {
            RenderTransform = new RotateTransform { Angle = angle };
        }
    }

}
