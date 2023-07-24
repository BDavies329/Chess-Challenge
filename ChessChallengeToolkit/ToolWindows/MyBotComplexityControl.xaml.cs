using System.Windows;
using System.Windows.Controls;

namespace ChessChallengeToolkit
{
    public partial class MyBotComplexityControl : UserControl
    {
        public MyBotComplexityControl()
        {
            InitializeComponent();
        }

        public void UpdateProgress(int tokenCount) {
            Bar.Value = tokenCount;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            VS.MessageBox.Show("ChessChallengeToolkit", "Button clicked");
        }
    }
}