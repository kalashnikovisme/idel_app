using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using idel_app.BisnessLogic;

namespace idel_app.DB {
    static public class Provider_DB {
        static public List<Provider> GetAllProviderFromDB() {
            List<Provider> list = new List<Provider>();
            for (int i = 0; i < 30; i++) {
                list.Add(new Provider());
            }
            return list;
        }

        static public string GetNameProviderById(int id) {
            if (id == 1) {
                return "Dark emperror";
            }
            return "NoBody";
        }

        static public List<List<string>> GetAllProvidersNames()
        {
            List<string> fields = new List<string>() { "Наименование", "Код" };
            List<List<string>> requests = CommandTo1C.requestToListLists(Program.v82Base, Program.connector, RequestTo1C.RequestGetAllProvidersNames, fields);
            return requests;
        }
    }
}