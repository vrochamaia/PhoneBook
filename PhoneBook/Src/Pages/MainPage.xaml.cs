using PhoneBook.Src.Framework;
using PhoneBook.Src.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Profile;
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
    public sealed partial class MainPage : Page {

        private const string HOME_MENU = "Principal";
        private const string CONTACTS_MENU = "Contatos";
        private const string GROUPS_MENU = "Grupos";


        public MainPage() {
            this.InitializeComponent();

            List<Menu> menuOptions = new List<Menu>();
            
            Menu contacts = new Menu(CONTACTS_MENU, "/Resources/Icons/ic_person_48pt.png");
            Menu groups = new Menu(GROUPS_MENU, "/Resources/Icons/ic_group_48pt.png");

            menuOptions.Add(contacts);
            menuOptions.Add(groups);

            listViewMenuOptions.ItemsSource = menuOptions;

            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {

            setSplitViewDisplayMode();

            Page page = e.Parameter as Page;

            if (page != null) {

                txtHeader.Text = App.lastPageHeader;
                mainContainer.Children.Add(page);

             }
            
        }

        private void onClickMenu(object sender, RoutedEventArgs e) {

            splitView.IsPaneOpen = !splitView.IsPaneOpen;    


        }

        private void onTappedListViewMenuOptions(object sender, TappedRoutedEventArgs e) {

            ListView listViewMenu = sender as ListView;
            
            if (mainContainer.Children.Count > 0)
                mainContainer.Children.RemoveAt(0);

            switch (listViewMenu.SelectedIndex) {

                case 0:

                ContactPage contactPage = new ContactPage();
                txtHeader.Text = CONTACTS_MENU;
                mainContainer.Children.Add(contactPage);
                break;

                case 1:

                GroupPage groupPage = new GroupPage();
                txtHeader.Text = GROUPS_MENU;
                mainContainer.Children.Add(groupPage);
                break;
                
            }

            splitView.IsPaneOpen = false;

        }

        private void setSplitViewDisplayMode() {

            var str = AnalyticsInfo.VersionInfo.DeviceFamily;

            if (str == "Windows.Desktop")
                splitView.DisplayMode = SplitViewDisplayMode.Inline;
            else if (str == "Windows.Mobile")
                splitView.DisplayMode = SplitViewDisplayMode.Overlay;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e) {
            base.OnNavigatedFrom(e);

            App.lastPageHeader = txtHeader.Text;

        }

        
    }
}
