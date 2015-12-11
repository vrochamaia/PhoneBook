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
    public sealed partial class EditContactPage : Page {

        public EditContactPage() {
            this.InitializeComponent();
            
        }

        private Contact contactSelected;

        protected override void OnNavigatedTo(NavigationEventArgs e) {

            contactSelected = e.Parameter as Contact;

            txtName.Text = contactSelected.Name;
            txtEmail.Text = contactSelected.Email;
            txtPhoneNumber.Text = contactSelected.PhoneNumber;
        }

        private void onKeyUpMask(object sender, KeyRoutedEventArgs e) {

            if (!e.Key.Equals(VirtualKey.Back)) {

                TextBox txtBox = sender as TextBox;
                string txtBoxValue = txtBox.Text;

                if (txtBox.Name.Equals(txtPhoneNumber.Name))
                    txtBoxValue = Util.phoneNumberMask(txtBoxValue);

                txtBox.Text = txtBoxValue;
                txtBox.SelectionStart = txtBox.Text.Length;

            }

        }

        private async void onClickBtnEditContact(object sender, RoutedEventArgs e) {


            string name = txtName.Text;
            string email = txtEmail.Text;
            string phoneNumber = txtPhoneNumber.Text;

            if (name.Equals("") || email.Equals("") || phoneNumber.Equals("")) {

                await Util.getDialog("Atenção", "Preencha todos os campos.").ShowAsync();
                return;
            }

            if (!Util.isValidEmail(email)) {
                await Util.getDialog("Atenção", "Digite um email válido.").ShowAsync();
                return;
            }


            Contact contact = new Contact();

            contact.ContactID = contactSelected.ContactID;
            contact.Name = name;
            contact.Email = email;
            contact.PhoneNumber = phoneNumber;
            contact.IsFavorite = false;

            ContactDAO contactDao = new ContactDAO();


            if (contactDao.update(contact))
                await Util.getDialog("Alteração Realizada", "Contato alterado com sucesso.").ShowAsync();
            else
                await Util.getDialog("Alteração não realizada", "Erro ao alterar contato.").ShowAsync();

            ContactPage contactPage = new ContactPage();
            Frame.Navigate(typeof(MainPage), contactPage);

        }

        private async void onClickBtnDeleteContact(object sender, RoutedEventArgs e) {

            MessageDialog dialog = new MessageDialog("Tem certeza que deseja excluir o contato?");

            UICommand btnYes = new UICommand();
            btnYes.Label = "Sim";

            UICommand btnNo = new UICommand();
            btnNo.Label = "Não";



            dialog.Commands.Add(btnYes);
            dialog.Commands.Add(btnNo);

            IUICommand result = await dialog.ShowAsync();

            if(result != null) {

                if (result.Label.Equals("Sim")) {

                    ContactDAO contactDao = new ContactDAO();

                    if (contactDao.delete(contactSelected)) {
                        Util.refreshNumericBadge(contactDao.getAll().Count());
                        await Util.getDialog("Contato deletado", "Contato deletado com sucesso.").ShowAsync();
                    }
                   
                    else
                        await Util.getDialog("Contato não deletado", "Tente novamente mais tarde.").ShowAsync();

                    ContactPage contactPage = new ContactPage();
                    Frame.Navigate(typeof(MainPage), contactPage);

                }

            }
            
        }

        private void onClickBtnBack(object sender, RoutedEventArgs e) {

            ContactPage contactPage = new ContactPage();
            Frame.Navigate(typeof(MainPage), contactPage);

        }


    }
}
