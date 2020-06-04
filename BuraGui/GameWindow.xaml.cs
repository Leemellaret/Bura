using System;
using System.Collections;
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
using System.Windows.Shapes;

namespace BuraGUI
{
    //card11.Source = new BitmapImage(new Uri(@"assets\Images\Cards\06-03.png", UriKind.Relative)); пример установки изображения


    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public string PlayerName { get; }
        public GameWindow(string playerName)
        {
            InitializeComponent();


            PlayerName = playerName;
            var phrases = new ResourceDictionary() { Source = new Uri("pack://application:,,,/PhrasesInChat-ru.xaml") };
            for (int i = 1; i <= phrases.Count; i++)
            {
                string key = "phrase" + i.ToString();
                Button b = new Button();
                b.Name = key;
                b.Content = phrases[key].ToString();
                b.Click += phraseButton_Click;

                availablePhrasesPanel.Children.Add(b);
            }

        }

        private void phraseButton_Click(object sender, RoutedEventArgs e)
        {
            var message = new TextBlock();
            message.Inlines.Add(new Run(PlayerName + ": ") { FontWeight = FontWeights.Bold, Foreground = Brushes.Green });
            message.Inlines.Add(new Run(((Button)sender).Content.ToString()) { Foreground = Brushes.White });

            chatPanel.Children.Add(message);
            chatPanelScroller.ScrollToEnd();
        }

        private void cardButton_Click(object sender, RoutedEventArgs e)
        {
            Button s = (Button)sender;
            if (Grid.GetRow(s) == 1)
            {
                Grid.SetRow(s, 2);
            }
            else if (Grid.GetRow(s) == 2)
            {
                Grid.SetRow(s, 1);
            }
        }

        private void makeFirstMoveButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void beatButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void closedThrowOffButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void openedThrowOffButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
