using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Shogi_UWP.Handlers
{
    class Hint: Grid
    {
        public Hint(Shogi.Engine.Places.Field field)
        {
            SetRow(this, field.Row);
            SetColumn(this, field.Column);
            BorderBrush = new SolidColorBrush(Windows.UI.Colors.Black);
            BorderThickness = new Thickness(2);
            Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
            IsHitTestVisible = false;
            if (field.Piece != null)
            {
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(77, 20, 85, 30));
                Children.Add(new Windows.UI.Xaml.Shapes.Ellipse
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 181, 136, 99)),
                    Height = Height * 1.2,
                    Width = Width * 1.2
                });
            }
            else
                Children.Add(new Windows.UI.Xaml.Shapes.Ellipse
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(77, 20, 85, 30)),
                    Height = 25,
                    Width = 25
                });
        }
    }
}
