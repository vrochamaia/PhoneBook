using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Src.Model {

    [SQLite.Net.Attributes.Table("tb_group_contacts")]
    class GroupContacts {

        [SQLite.Net.Attributes.PrimaryKey,SQLite.Net.Attributes.AutoIncrement]
        public int GroupContactID { get; set; }
        public int GroupID { get; set; }
        public int ContactID { get; set; }
    }
}
