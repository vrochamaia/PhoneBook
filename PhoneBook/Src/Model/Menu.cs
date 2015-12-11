using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PhoneBook.Src.Model {

    class Menu {

        public string Name { get; set; }
        public string IconSource { get; set; }

        public Menu(string name, string iconSource) {

            this.Name = name;
            this.IconSource = iconSource;
        }

    }
}
