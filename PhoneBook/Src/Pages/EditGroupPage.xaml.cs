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
    public sealed partial class EditGroupPage : Page {
        public EditGroupPage() {
            this.InitializeComponent();
        }

        Group groupSelected;
        List<GroupContacts> groupContactsSelected = new List<GroupContacts>();
        
        List<Contact> contacts = new List<Contact>();
        List<Contact> contactsSelected = new List<Contact>();

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            
            groupSelected = e.Parameter as Group;
        
            ContactDAO contactDao = new ContactDAO();
            contacts = contactDao.getAll();

            GroupContactsDAO groupContactsDao = new GroupContactsDAO();
            groupContactsSelected = groupContactsDao.select(groupSelected.GroupID);
 
            listViewContacts.ItemsSource = contacts;
            
            txtName.Text = groupSelected.Name;
        }
        private async void onClickBtnUpdateGroup(object sender, RoutedEventArgs e) {


            string groupName = txtName.Text;

            if (groupName.Equals("")) {

                await Util.getDialog("Atenção", "Digite um nome para o grupo.").ShowAsync();
                return;
            }

            if (contactsSelected.Count == 0) {
                await Util.getDialog("Atenção", "Você tem que adicionar pelo menos um contato.").ShowAsync();
                return;
            }

            string groupContactsName = "";

            foreach (Contact contact in contactsSelected) {
                groupContactsName += contact.Name + ", ";
            }

            groupContactsName = groupContactsName.Remove(groupContactsName.Length - 2);


            Group group = new Group();
            group.GroupID = groupSelected.GroupID;
            group.Name = groupName;
            group.GroupContactsName = groupContactsName;

            GroupDAO groupDao = new GroupDAO();

            if(groupDao.update(group)) {

                GroupContactsDAO groupContactDao = new GroupContactsDAO();

                foreach (GroupContacts groupContactSelected in groupContactsSelected) {
                    groupContactDao.delete(groupContactSelected);
                }

                foreach (Contact contact in contactsSelected) {
                    
                    GroupContacts groupContact = new GroupContacts();

                    groupContact.GroupID = groupSelected.GroupID;
                    groupContact.ContactID = contact.ContactID;

                    groupContactDao.insert(groupContact);


                }

                await Util.getDialog("Grupo Editado", "O grupo foi editado com sucesso.").ShowAsync();


            } else
                await Util.getDialog("Atenção", "Erro ao editar grupo, tente novamente mais tarde.").ShowAsync();



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
                
            } else {

                img.Visibility = Visibility.Visible;
                contactsSelected.Add(contact);

            }


        }

        private async void onClickBtnDelete(object sender, RoutedEventArgs e) {

            MessageDialog dialog = new MessageDialog("Tem certeza que deseja excluir o grupo?");

            UICommand btnYes = new UICommand();
            btnYes.Label = "Sim";

            UICommand btnNo = new UICommand();
            btnNo.Label = "Não";



            dialog.Commands.Add(btnYes);
            dialog.Commands.Add(btnNo);

            IUICommand result = await dialog.ShowAsync();

            if (result != null) {

                if (result.Label.Equals("Sim")) {


                    GroupContactsDAO groupContactsDao = new GroupContactsDAO();

                    foreach(GroupContacts groupContact in  groupContactsSelected) {

                        groupContactsDao.delete(groupContact);
                    }

                    GroupDAO groupDao = new GroupDAO();

                    if(groupDao.delete(groupSelected)) 
                       await Util.getDialog("Grupo deletado", "Grupo deletado com sucesso.").ShowAsync();
                    else
                        await Util.getDialog("Grupo não deletado", "Tente novamente mais tarde.").ShowAsync();
                    

                    GroupPage groupPage = new GroupPage();
                    Frame.Navigate(typeof(MainPage), groupPage);

                }

            }

        }

        private void onClickBtnBack(object sender, RoutedEventArgs e) {

            GroupPage groupPage = new GroupPage();
            Frame.Navigate(typeof(MainPage), groupPage);

        }


    }
}
