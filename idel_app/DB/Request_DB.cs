using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using idel_app.BisnessLogic;

namespace idel_app.DB {
  static public class Request_DB {
    /// <summary>
    /// Если в БД будет сущность Request, у которой будут все те же поля, что у меня в классе и они будут расположены в том же порядке, что у 
    /// у меня в классе, тогда твоя задача просто сделать функцию (она ниже), которая будет переделывать из БД в список списков стрингов, дальше всё должно само по себе произойти.
    /// Если нет, буди звонком.
    /// </summary>
    /// <returns></returns>
    static private List<List<string>> GetAllRequestToListList() {
      return new List<List<string>>();
    }

    static private int PInt(string s) {
      return Int32.Parse(s);
    }

    static private DateTime PDT(string s) {
      return DateTime.Parse(s);
    }

    static private bool PB(string s) {
      return Boolean.Parse(s);
    }

    static public List<Request> GetAllRequestFromDB() {
      List<List<string>> workList = GetAllRequestToListList();
      List<Request> list = new List<Request>();
      foreach (List<string> l in workList) {
        list.Add(new Request(PInt(l[0]), l[1], PDT(l[2]), PDT(l[3]), l[4], l[5], l[6], PInt(l[7]), PB(l[8]), PB(l[9]), l[10]));
      }
      return list;
    }
  }
}