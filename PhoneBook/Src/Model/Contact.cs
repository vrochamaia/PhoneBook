using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PhoneBook.Src.Model {

    [SQLite.Net.Attributes.Table("tb_contacts")]
    class Contact {

        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int ContactID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsFavorite { get; set; }
        public string SrcFavoriteImage {
            get {

                if (this.IsFavorite)
                    return "/Resources/Icons/gold_star.png";
                else
                    return "/Resources/Icons/blank_star.png";


            } }


    }
}
