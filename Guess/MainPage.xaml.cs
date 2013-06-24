using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace Guess
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            BuildGameList();
        }

        private void BuildGameList()
        {
            GameList.ItemsSource = App.GameTypeList;
        }

        private void Game_Tapped(object sender, MouseButtonEventArgs e)
        {
            Grid g = sender as Grid;
            GameType gt = g.DataContext as GameType;
            NavigationService.Navigate(new Uri("/CardPage.xaml?game=" + gt.SortOrder, UriKind.Relative));
        }
    }
}