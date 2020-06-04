using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BuraGUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class HelloWindow : Window
    {
        public HelloWindow()
        {
            InitializeComponent();
        }

        private void startGameButton_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow("Roma");
            gameWindow.Owner = this;
            /*gameWindow.Height = Height;
            gameWindow.Width = Width;
            gameWindow.WindowState = WindowState;*/
            this.Visibility = Visibility.Hidden;

            gameWindow.ShowDialog();

            /*Height = gameWindow.Height;
            Width = gameWindow.Width;
            WindowState = gameWindow.WindowState;*/
            this.Visibility = Visibility.Visible;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // закрытие окна startGameButton_Click
        }
    }
}
