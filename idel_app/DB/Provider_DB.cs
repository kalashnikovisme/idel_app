using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using idel_app.BisnessLogic;

namespace idel_app.DB {
  public class Provider_DB {
    public Provider_DB() {

    }

    public List<Provider> GetAllProviderFromDB() {
      List<Provider> list = new List<Provider>();
      for (int i = 0; i < 30; i++) {
        list.Add(new Provider());
      }
      return list;
    }
  }
}