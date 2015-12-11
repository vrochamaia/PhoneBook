using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Src.Interface {
    interface DAO<M>: IDisposable where M : class , new() {

        bool insert(M model);
        bool update(M model);
        bool delete(M model);
        List<M> select(object parameter);
        List<M> getAll();
    }
}
