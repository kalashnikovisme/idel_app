using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idel_app.Middle {
  /// <summary>
  /// Этот класс будет организовывать передачу данных между интерфейсом и "внутренностями"
  /// </summary>
  public class MiddleClass {
    public List<List<string>> AllRequests() {
      List<List<string>> list = new List<List<string>>();
      for (int i = 0; i < 30; i++) {
        list.Add(new List<string>() { "1", "name", "employee" });
      }
      return list;
    }

    public List<string> RequestFields() {
      return new List<string>() { "id", "название", "сотрудник" };
    }
  }
}