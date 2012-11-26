using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using idel_app.BisnessLogic;
using V82;

namespace idel_app.DB {
  static public class Request_DB {
    /// <summary>
    /// Если в БД будет сущность Request, у которой будут все те же поля, что у меня в классе и они будут расположены в том же порядке, что у 
    /// у меня в классе, тогда твоя задача просто сделать функцию (она ниже), которая будет переделывать из БД в список списков стрингов, дальше всё должно само по себе произойти.
    /// Если нет, буди звонком.
    /// </summary>ок
    /// <returns></returns>
    static private List<List<string>> GetAllRequestToListList() {

        // создание COM объекта для соединения с 1С
        COMConnectorClass connector = new COMConnectorClass();//мб лучше сделать полем класса
        // объект подключения к базе
        object v82Base = null;//мб лучше сделать полем класса

        bool connect = RequestTo1C.Connect1C(ref v82Base, ref connector);

        List<List<string>> requests = RequestTo1C.FillListListRequests(v82Base, connector);


      return requests;
    }    

    static private int PInt(string s) {
      return Int32.Parse(s);
    }

    static private DateTime PDT(string s) {
      return DateTime.Parse(s);
    }

    static private bool Parse1CString(string s) {
      if (s == "Истина") {
        return true;
      }
      if (s == "Ложь") {
        return false;
      }
      return false;
    }

    static public List<Request> GetAllRequestFromDB() {
      List<List<string>> workList = GetAllRequestToListList();
      List<Request> list = new List<Request>();
      foreach (List<string> l in workList) {
        list.Add(new Request(PInt(l[0]), l[1], PDT(l[2]), PDT(l[3]), l[4], l[5], l[6], PInt(l[7]), Parse1CString(l[8]), Parse1CString(l[9]), l[10]));
      }
      return list;
    }
  }
}