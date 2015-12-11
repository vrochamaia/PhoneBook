using PhoneBook.Src.Framework;
using PhoneBook.Src.Interface;
using PhoneBook.Src.Model;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Src.DAO {

    class ContactDAO : DAO<Contact> {

        SQLiteConnection connection;
        private const string TB_NAME = "tb_contacts";

        public ContactDAO() {
            connection = new SQLiteConnection(new SQLitePlatformWinRT(),App.DB_PATH);
        }

        public bool delete(Contact model) {

            try {

                var contact = connection.Query<Contact>("SELECT * FROM " + TB_NAME + " WHERE contactID =" + model.ContactID).FirstOrDefault();

                if (contact != null) {

                    connection.RunInTransaction(() => {

                        connection.Delete(contact);

                    });

                    return true;

                }

                return false;

            } catch (Exception e) {
                Debug.WriteLine("Delete error: " + e.ToString());
                return false;
            }

        }

        public void Dispose() {
            throw new NotImplementedException();

        }

        public List<Contact> getAll() {

            List<Contact> contacts = new List<Contact>();

            try {

                contacts = connection.Table<Contact>().OrderBy(c => c.Name).ToList();

            } catch(Exception e) {
                Debug.WriteLine("Sqlite error: "+e.ToString());
                
            }

            return contacts;

        }

        public bool insert(Contact model) {
            
            try {

                connection.RunInTransaction(() => {
                    connection.Insert(model);
                });

                return true;

            } catch(Exception e) {
                Debug.WriteLine("Insert error: " + e.ToString());
                return false;
            }


           
        }

        public List<Contact> select(object parameter) {


            string selectQuery = "";

            if (parameter.GetType() == typeof(bool))
                selectQuery = "SELECT * FROM " + TB_NAME + " WHERE isFavorite = 1";

            else if (parameter.GetType() == typeof(string))
                selectQuery = "SELECT * FROM " + TB_NAME + " WHERE name LIKE ?||'%'";
            else if (parameter.GetType() == typeof(int))
                selectQuery = "SELECT * FROM " + TB_NAME + " WHERE contactID = ?";

            List<Contact> contacts = new List<Contact>();

           
            try {
   
                contacts = connection.Query<Contact>(selectQuery,parameter).OrderBy(c=>c.Name).ToList();
                
            } catch (Exception e) {
                Debug.WriteLine("SQLite error: " + e.ToString());
            }


            return contacts;

        }

        public bool update(Contact model) {

            try {

                var contact = connection.Query<Contact>("SELECT contactID,name,email,phoneNumber FROM " +TB_NAME +" WHERE contactID =" + model.ContactID).FirstOrDefault();

                if(contact != null) {

                    contact.Name = model.Name;
                    contact.Email = model.Email;
                    contact.PhoneNumber = model.PhoneNumber;

                    connection.RunInTransaction( () => {

                        connection.Update(contact);

                    });

                    return true;

                }

                return false;
                
            } catch (Exception e) {
                Debug.WriteLine("Update error: " + e.ToString());
                return false;
            }

        }

        public bool changeFavoriteStatus(Contact model) {

            try {

                var contact = connection.Query<Contact>("SELECT * FROM " + TB_NAME + " WHERE contactID =" + model.ContactID).FirstOrDefault();

                if (contact != null) {

                    contact.IsFavorite = model.IsFavorite;

                    connection.RunInTransaction(() => {

                        connection.Update(contact);

                    });

                    return true;

                }

                return false;

            } catch (Exception e) {
                Debug.WriteLine("Update error: " + e.ToString());
                return false;
            }

        }

        public async void changeTileContent() {

            List<Contact> contacts = getAll();

            if(contacts.Count > 0) {

                Util.refreshTile(contacts.FirstOrDefault().Name);
                await Task.Delay(30000);
                Util.refreshTile(contacts.LastOrDefault().Name);
                await Task.Delay(30000);
                changeTileContent();


            }

            

        }



    }
}
