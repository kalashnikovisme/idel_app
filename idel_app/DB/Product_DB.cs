using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using idel_app.BisnessLogic;

namespace idel_app.DB {
  public class Product_DB {
    static public List<Product> GetAllProductFromDB() {
      List<Product> list = new List<Product>();
      for (int i = 0; i < 30; i++) {
        list.Add(new Product());
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