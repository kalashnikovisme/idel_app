using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using idel_app.BisnessLogic;

namespace idel_app.DB {
  public class Request_DB {
    public Request_DB() {

    }

    public List<Request> GetAllRequestFromDB() {
      List<Request> list = new List<Request>();
      for (int i = 0; i < 30; i++) {
        list.Add(new Request());
      }
      return list;
    }
  }
}