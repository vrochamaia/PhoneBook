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
    class GroupDAO : DAO<Group> {

        SQLiteConnection connection;
        private const string TB_NAME = "tb_groups";

        public GroupDAO() {
            connection = new SQLiteConnection(new SQLitePlatformWinRT(), App.DB_PATH);
        }

        public bool delete(Group model) {

            try {

                var group = connection.Query<Group>("SELECT * FROM " + TB_NAME + " WHERE groupID =" + model.GroupID).FirstOrDefault();

                if (group != null) {

                    connection.RunInTransaction(() => {

                        connection.Delete(group);

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

        public List<Group> getAll() {

            List<Group> groups = new List<Group>();

            try {
                
                groups = connection.Table<Group>().OrderBy(g => g.Name).ToList();

                



            } catch (Exception e) {
                Debug.WriteLine("Sqlite error: " + e.ToString());

            }

            return groups;
        }

        public bool insert(Group model) {

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

        public List<Group> select(object parameter) {

            string selectQuery = "SELECT * FROM " + TB_NAME + " WHERE name LIKE ?||'%'";

            List<Group> groups = new List<Group>();


            try {

                groups = connection.Query<Group>(selectQuery, parameter);

            } catch (Exception e) {
                Debug.WriteLine("SQLite error: " + e.ToString());
            }


            return groups;
        }

        public bool update(Group model) {

            try {

                var group = connection.Query<Group>("SELECT groupID,name,groupContactsName FROM " + TB_NAME + " WHERE groupID =" + model.GroupID).FirstOrDefault();

                if (group != null) {

                    group.Name = model.Name;
                    group.GroupContactsName = model.GroupContactsName;


                    connection.RunInTransaction(() => {

                        connection.Update(group);

                    });

                    return true;

                }

                return false;

            } catch (Exception e) {
                Debug.WriteLine("Update error: " + e.ToString());
                return false;
            }
        }
    }
}
