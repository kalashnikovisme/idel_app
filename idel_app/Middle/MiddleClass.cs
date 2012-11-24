using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idel_app.Middle {
  /// <summary>
  /// Этот класс будет организовывать передачу данных между интерфейсом и "внутренностями"
  /// </summary>
  public class MiddleClass {
    Request.Request request;
    public List<List<string>> AllRequests() {
      List<List<string>> list = new List<List<string>>();
      string[] str = new string[RequestFields().Count];
      for (int i = 0; i < str.Length; i++) {
        str[i] = "1";
      }
      for (int i = 0; i < 30; i++) {
        list.Add(str.ToList<string>());
      }
      return list;
    }

    public List<string> RequestFields() {
      Type type = typeof(Request.Request);
      var foo = Activator.CreateInstance(type);
      List<string> list = new List<string>();
      foreach (System.Reflection.PropertyInfo p in foo.GetType().GetProperties()) {
        list.Add(p.Name);
      }
      return list;
    }

    public void AddNewRequest(List<string> newAdd) {
      List<List<string>> list = AllRequests();
      list.Add(newAdd);
    }

    public void DeleteRequestByIndex(int index) {
      
    }

    public void DeleteAll() {

    }

    public void DeletePassedRequests() {

    }

    public void SaveChanges(List<List<string>> changes) {

    }

    public void MarkRequestPassed(int index) {

    }

    public void MarkRequestUnPassed(int index) {

    }
  }
}