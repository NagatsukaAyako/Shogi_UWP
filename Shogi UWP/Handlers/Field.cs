using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace Shogi_UWP.Handlers
{
    class Field: Grid
    {
        public Shogi.Engine.Places.Field FieldEngine;
        public Field(Shogi.Engine.Places.Field field)
        {
            FieldEngine = field;
            PointerPressed += delegate (object sender, PointerRoutedEventArgs e) { Shogi.Engine.ShogiEngine.TrySelectField(field); };
            BorderThickness = new Thickness(2);
            Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
            BorderBrush = new SolidColorBrush(Windows.UI.Colors.Black);
        }
    }
}
