using PhoneBook.Src.DAO;
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
using Windows.System;
using Windows.UI.Popups;
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
    public sealed partial class ContactPage : Page {

        private const string BTN_EDIT_LABEL = "Editar";
        private const string BTN_FAVORITE_LABEL = "Favoritar";
        private const string BTN_CANCEL_FAVORITE_LABEL = "Retirar dos favoritos";


        public ContactPage() {
            this.InitializeComponent();

            refreshListViewContacts();
            
        }

        private void refreshListViewContacts() {

            progressBarListViewContacts.Visibility = Visibility.Visible;

            ContactDAO contactDao = new ContactDAO();

            List<Contact> contacts = contactDao.getAll();

            if (contacts.Count == 0)
                txtNoContactsFounded.Visibility = Visibility.Visible;
            else
                txtNoContactsFounded.Visibility = Visibility.Collapsed;


            progressBarListViewContacts.Visibility = Visibility.Collapsed;
            listViewContacts.ItemsSource = contacts;

        }

        

        private void onClickBtnSearch(object sender, RoutedEventArgs e) {

            progressBarListViewContacts.Visibility = Visibility.Visible;


            ContactDAO contactDao = new ContactDAO();
          
            object searchedValue = "";


            if ((bool)btnRadioName.IsChecked) {

                searchedValue = txtSearchedValue.Text;

                if (searchedValue.Equals("")) {
                    refreshListViewContacts();
                    return;
                }

                

            } else if ((bool)btnRadioFavorite.IsChecked)
                searchedValue = true;


            List<Contact> contacts = new List<Contact>();
            contacts = contactDao.select(searchedValue);

            if (contacts.Count == 0)
                txtNoContactsFounded.Visibility = Visibility.Visible;
            else
                txtNoContactsFounded.Visibility = Visibility.Collapsed;


            listViewContacts.ItemsSource = contacts;
            progressBarListViewContacts.Visibility = Visibility.Collapsed;
        }

        private void onClickBtnCreateContact(object sender, RoutedEventArgs e) {

            Frame frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(CreateContactPage));
        }

        private void onClickRadioButton(object sender, RoutedEventArgs e) {

            RadioButton radioButton = sender as RadioButton;

            if (radioButton.Content.Equals(btnRadioFavorite.Content)) {

                txtSearchedValue.IsEnabled = false;
                txtSearchedValue.Text = "";
                txtSearchedValue.PlaceholderText = "Clique no botão ao lado";

            } else {

                 txtSearchedValue.PlaceholderText = "Digite o nome do contato";
                 txtSearchedValue.IsEnabled = true;

            }
        }


        private void onKeyUpTxtSearchedValue(object sender, KeyRoutedEventArgs e) {
            
            if (e.Key.Equals(VirtualKey.Enter))
                onClickBtnSearch(sender, null);

        }


        private async void listViewContacts_Tapped(object sender, TappedRoutedEventArgs e) {

            


            ListView contactsListView = sender as ListView;
            Contact contact = (Contact)contactsListView.SelectedItem;

            ContactDAO contactDao = new ContactDAO();

            if (contact != null) {

                // Mudar favoritar para o clique na estrela(botao)
                MessageDialog dialog = new MessageDialog("Escolha uma das opções abaixo:");

                UICommand btnEdit = new UICommand();
                btnEdit.Label = BTN_EDIT_LABEL;

                UICommand btnFavorite = new UICommand();

                if (contact.IsFavorite)
                    btnFavorite.Label = BTN_CANCEL_FAVORITE_LABEL;
                else
                    btnFavorite.Label = BTN_FAVORITE_LABEL;


                dialog.Commands.Add(btnEdit);
                dialog.Commands.Add(btnFavorite);

                IUICommand result = await dialog.ShowAsync();

                if(result != null) {

                    switch (result.Label) {

                        case BTN_EDIT_LABEL:
                        Frame frame = Window.Current.Content as Frame;
                        frame.Navigate(typeof(EditContactPage), contact);
                        break;

                        case BTN_FAVORITE_LABEL:
                        contact.IsFavorite = true;
                        contactDao.changeFavoriteStatus(contact);
                        refreshListViewContacts();

                        break;

                        case BTN_CANCEL_FAVORITE_LABEL:
                        contact.IsFavorite = false;
                        contactDao.changeFavoriteStatus(contact);
                        refreshListViewContacts();
                        break;

                        default:
                        break;

                    }
                }


                
            }
        }
    }
}
