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
    class GroupContactsDAO : DAO<GroupContacts> {

        SQLiteConnection connection;
        private const string TB_NAME = "tb_group_contacts";

        public GroupContactsDAO() {
            connection = new SQLiteConnection(new SQLitePlatformWinRT(), App.DB_PATH);
           
        }


        public bool delete(GroupContacts model) {

            try {

                var groupContact = connection.Query<GroupContacts>("SELECT * FROM " + TB_NAME + " WHERE groupContactID =" + model.GroupContactID).FirstOrDefault();

                if (groupContact != null) {

                    connection.RunInTransaction(() => {

                        connection.Delete(groupContact);

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

        public List<GroupContacts> getAll() {
            throw new NotImplementedException();
        }

        public bool insert(GroupContacts model) {

            try {

                connection.RunInTransaction(() => {
                    connection.Insert(model);
                });

                return true;

            } catch (Exception e) {
                Debug.WriteLine("Insert error: " + e.ToString());
                return false;
            }
        }

        public List<GroupContacts> select(object parameter) {

            List<GroupContacts> groupContacts = new List<GroupContacts>();


            string selectQuery = "";


            selectQuery = "SELECT * FROM " + TB_NAME + " WHERE groupID = ?";
            
            try {

                groupContacts = connection.Query<GroupContacts>(selectQuery, parameter).ToList();

            } catch (Exception e) {
                Debug.WriteLine("SQLite error: " + e.ToString());
            }


            return groupContacts;
            
        }

        public bool update(GroupContacts model) {
            throw new NotImplementedException();
        }

       


    }
}
