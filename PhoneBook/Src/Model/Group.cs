using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Src.Model {

    [SQLite.Net.Attributes.Table("tb_groups")]
    class Group {

        

        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int GroupID { get; set; }
        public string Name { get; set; }
        public string GroupContactsName { get; set; }

    }
}
