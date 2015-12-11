using PhoneBook.Src.DAO;
using PhoneBook.Src.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PhoneBook.Src.Pages {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GroupPage : Page {

        List<Group> groups = new List<Group>();

        public GroupPage() {

            this.InitializeComponent();

            GroupDAO groupDao = new GroupDAO();
            groups = groupDao.getAll();

        
            if (groups.Count == 0)
                txtNoGroupsFounded.Visibility = Visibility.Visible;

            listViewGroups.ItemsSource = groups;


        }

        private void onClickBtnSearch(object sender, RoutedEventArgs e) {

            progressBarListViewGroups.Visibility = Visibility.Visible;


            GroupDAO groupDao = new GroupDAO();

            string searchedValue = txtSearchedValue.Text;

            List<Group> groups = new List<Group>();
            groups = groupDao.select(searchedValue);

            if (groups.Count == 0)
                txtNoGroupsFounded.Visibility = Visibility.Visible;
            else
                txtNoGroupsFounded.Visibility = Visibility.Collapsed;


            listViewGroups.ItemsSource = groups;
            progressBarListViewGroups.Visibility = Visibility.Collapsed;

        }

        private void listViewGroups_Tapped(object sender, TappedRoutedEventArgs e) {

            Group groupSelected = (sender as ListView).SelectedItem as Group;

            Frame frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(EditGroupPage), groupSelected);

        }

        private void onClickBtnCreateGroup(object sender, RoutedEventArgs e) {

            Frame frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(CreateGroupPage));
        }

        private void onKeyUpTxtSearchedValue(object sender, KeyRoutedEventArgs e) {

            if (e.Key.Equals(VirtualKey.Enter))
                onClickBtnSearch(sender, null);

        }
    }
}
