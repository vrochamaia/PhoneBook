using PhoneBook.Src.DAO;
using PhoneBook.Src.Framework;
using PhoneBook.Src.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class CreateGroupPage : Page {


        public CreateGroupPage() {
            this.InitializeComponent();
        }

        List<Contact> contacts;
        List<Contact> contactsSelected = new List<Contact>();

        protected override void OnNavigatedTo(NavigationEventArgs e) {

            ContactDAO contactDao = new ContactDAO();
            contacts = contactDao.getAll();

            if (contacts.Count == 0)
                txtNoContactsFounded.Visibility = Visibility.Visible;


            listViewContacts.ItemsSource = contactDao.getAll();

        }

        private async void onClickBtnCreateGroup(object sender, RoutedEventArgs e) {

            string groupName = txtName.Text;

            if(groupName.Equals("")) {

                await Util.getDialog("Atenção", "Digite um nome para o grupo.").ShowAsync();
                return;
            }

            if(contactsSelected.Count == 0) {
                await Util.getDialog("Atenção", "Você tem que adicionar pelo menos um contato.").ShowAsync();
                return;
            }

        

            string groupContactsName = "";

            foreach(Contact contact in contactsSelected) {
                groupContactsName += contact.Name + ", ";
            }

            groupContactsName = groupContactsName.Remove(groupContactsName.Length - 2);

            Group group = new Group();
            group.Name = groupName;
            group.GroupContactsName = groupContactsName;

            GroupDAO groupDao = new GroupDAO();

            if(groupDao.insert(group)) {

                int groupID = groupDao.getAll().LastOrDefault().GroupID;
                GroupContactsDAO groupContactDao = new GroupContactsDAO();

                ContactDAO contactDao = new ContactDAO();
                foreach(Contact contact in contactsSelected) {

                    
                    GroupContacts groupContact = new GroupContacts();

                    groupContact.GroupID = groupID;
                    groupContact.ContactID = contact.ContactID;

                    groupContactDao.insert(groupContact);


                }

                await Util.getDialog("Grupo Criado", "O grupo foi criado com sucesso.").ShowAsync();


            } else 
                await Util.getDialog("Atenção", "Erro ao adicionar grupo, tente novamente mais tarde.").ShowAsync();

            


            GroupPage groupPage = new GroupPage();
            Frame.Navigate(typeof(MainPage), groupPage);

        }


        private void listViewContacts_ItemClick(object sender, ItemClickEventArgs e) {

            var item = e.ClickedItem;

            var itemContainer = listViewContacts.ContainerFromItem(item);

            var img = (Image)Util.FindChildByName(itemContainer, "imgChecked");

            Contact contact = item as Contact;
            

            if (img.Visibility == Visibility.Visible) {

                img.Visibility = Visibility.Collapsed;
                contactsSelected.Remove(contact);
            }
               
            else {
                
                img.Visibility = Visibility.Visible;
                contactsSelected.Add(contact);

            }


        }


        private void onClickBtnBack(object sender, RoutedEventArgs e) {

            GroupPage groupPage = new GroupPage();
            Frame.Navigate(typeof(MainPage), groupPage);

        }
    }
}
