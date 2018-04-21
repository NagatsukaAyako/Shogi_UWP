using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Shogi_UWP.Handlers
{
    class Last : Grid
    {
        public Last()
        {
            BorderBrush = new SolidColorBrush(Windows.UI.Colors.Black);
            BorderThickness = new Thickness(2);
            Background = new SolidColorBrush(Windows.UI.Color.FromArgb(105, 155, 199, 0));
            IsHitTestVisible = false;
        }
    }
}
