using Microsoft.Maui.Controls;
using SharpHook;
using SharpHook.Native;
using System.Drawing;

namespace _2048
{
    public partial class MainPage : ContentPage
    {

        private TaskPoolGlobalHook _hook;
        public MainPage()
        {
            InitializeComponent();
            _hook = new TaskPoolGlobalHook();
            _hook.KeyPressed += OnKeyPressedAsync;
            _hook.RunAsync();
            StartGame();
        }

        private async void OnKeyPressedAsync(object? sender, KeyboardHookEventArgs e)
        {
            var labels = board.Children.OfType<Label>();
            var tiles = board.Children.OfType<BoxView>();

            if (e.Data.KeyCode == KeyCode.VcUp)
            {
                // Go up
                var movesToTop = 0;
                var positionToCheck = tiles.ElementAt(0 + 16).Y - 125 - (125 * movesToTop);
                while (!tiles.Any(x => x.Y == positionToCheck) && positionToCheck > 0)
                {
                    movesToTop += 1;
                    positionToCheck = tiles.ElementAt(0 + 16).Y - 125 - (125 * movesToTop);
                }
                labels.ElementAt(0).TranslateTo(0, -125 * movesToTop, 100, Easing.Linear);
                await tiles.ElementAt(0 + 16).TranslateTo(0, -125 * movesToTop, 100, Easing.Linear);
            }
            if (e.Data.KeyCode == KeyCode.VcRight)
            {
                // Go right
                var movesToRight = 0;
                var positionToCheck = tiles.ElementAt(0 + 16).X + 125 + (125 * movesToRight);
                while (!tiles.Any(x => x.X == positionToCheck) && positionToCheck < 505)
                {
                    movesToRight += 1;
                    positionToCheck = tiles.ElementAt(0 + 16).X + 125 + (125 * movesToRight);
                }
                labels.ElementAt(0).TranslateTo(125 * movesToRight, 0, 100, Easing.Linear);
                await tiles.ElementAt(0 + 16).TranslateTo(125 * movesToRight, 0, 100, Easing.Linear);
            }
            if (e.Data.KeyCode == KeyCode.VcDown)
            {
                // Go up
                var movesToBottom = 0;
                var positionToCheck = tiles.ElementAt(0 + 16).Y + 125 + (125 * movesToBottom);
                while (!tiles.Any(x => x.Y == positionToCheck) && positionToCheck < 505)
                {
                    movesToBottom += 1;
                    positionToCheck = tiles.ElementAt(0 + 16).Y + 125 + (125 * movesToBottom);
                }
                labels.ElementAt(0).TranslateTo(0, 125 * movesToBottom, 100, Easing.Linear);
                await tiles.ElementAt(0 + 16).TranslateTo(0, 125 * movesToBottom, 100, Easing.Linear);
            }
            if (e.Data.KeyCode == KeyCode.VcLeft)
            {
                // Go left
                var movesToLeft = 0;
                var positionToCheck = tiles.ElementAt(0 + 16).X - 125 - (125 * movesToLeft);
                while (!tiles.Any(x => x.X == positionToCheck) && positionToCheck > 0)
                {
                    movesToLeft += 1;
                    positionToCheck = tiles.ElementAt(0 + 16).X - 125 - (125 * movesToLeft);
                }
                labels.ElementAt(0).TranslateTo(-125 * movesToLeft, 0, 100, Easing.Linear);
                await tiles.ElementAt(0 + 16).TranslateTo(-125 * movesToLeft, 0, 100, Easing.Linear);
            }

            // Add correct margin to right tiles
            foreach(var tile in tiles.Where(x => x.TranslationX == 375))
            {
                tile.ScaleXTo(0.95, 0);
                tile.TranslateTo(tile.TranslationX - 3, 0, 0);
            };
            foreach (var label in labels.Where(x => x.TranslationX == 375))
            {
                label.TranslateTo(label.TranslationX - 3, 0, 0);
            };

            // Add correct margin to left tiles

            // Add correct margin to top tiles

            // Add correct margin to bottom tiles
        }

        private void StartGame()
        {
            // Generate a start tile
            GenerateTile(2);
        }

        private void GenerateTile(int tileNumber)
        {
            var newLabel = new Label();
            var newBoxView = new BoxView();

            newLabel.Text = tileNumber.ToString();
            newLabel.VerticalOptions = LayoutOptions.Center;
            newLabel.HorizontalOptions = LayoutOptions.Center;
            newLabel.FontSize = 48;
            newLabel.TextColor = Microsoft.Maui.Graphics.Color.FromRgba("#776E65");
            newLabel.FontAttributes = FontAttributes.Bold;

            newBoxView.BackgroundColor = Microsoft.Maui.Graphics.Color.FromRgba("#EEE4DA");
            newBoxView.Margin = new Thickness(6, 12, 6, 6);
            newBoxView.CornerRadius = 10;

            board.Children.Add(newBoxView);
            board.Children.Add(newLabel);
        }

        static void OnKeyPressed(KeyboardHookEventArgs e)
        {
            
        }
    }
}