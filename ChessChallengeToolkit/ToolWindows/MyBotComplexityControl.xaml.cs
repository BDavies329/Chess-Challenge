using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChessChallengeToolkit
{
    public partial class MyBotComplexityControl : UserControl
    {
        public MyBotComplexityControl()
        {
            InitializeComponent();
            SetFontSizeBasedOnPaneSize();
        }

        public void UpdateProgress(int tokenCount)
        {
            Bar.Value = tokenCount;

            var t = (double)tokenCount / 1024;

            if (t <= 0.7)
                Bar.Foreground = Brushes.Gray;
            else if (t <= 0.85)
                Bar.Foreground = Brushes.Yellow;
            else if (t <= 1)
                Bar.Foreground = Brushes.Orange;
            else
                Bar.Foreground = Brushes.Red;

            ComplexityLabel.Content = $"Bot Brain Capacity: {tokenCount}/{1024}";
            if (tokenCount > 1024)
            {
                ComplexityLabel.Content += " [LIMIT EXCEEDED]";
            }
        }

        private void MyBotComplexityControlSizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetFontSizeBasedOnPaneSize();
        }

        private void SetFontSizeBasedOnPaneSize() {
            if(this.ActualHeight > 0)
            {
                ComplexityLabel.FontSize = Math.Min(this.ActualHeight / 2, this.ActualWidth / 10);
            }
        }

    }
}