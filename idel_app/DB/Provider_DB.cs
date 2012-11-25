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
  }
}