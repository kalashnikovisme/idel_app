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
      return new List<List<string>>();
    }

    public List<string> RequestFields() {
      return new List<string>() { "id", "название", "сотрудник" };
    }
  }
}