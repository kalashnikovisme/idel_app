using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using idel_app.Middle;

namespace idel_app.Request {
  public class Request {
    private int id = Const.THERE_IS_NOT;
    public int Id {
      get {
        return id;
      }
      set {
        id = value;
      }
    }

    private string title = "";
    public string Title {
      get {
        return title;
      }
      set {
        title = value;
      }
    }

    private DateTime createDate = new DateTime();
    public DateTime CreateDate {
      get {
        return createDate;
      }
      set {
        createDate = value;
      }
    }

    private DateTime passDate = new DateTime();
    public DateTime PassDate {
      get {
        return passDate;
      }
      set {
        passDate = value;
      }
    }

    private string employee = "";
    public string Employee {
      get {
        return employee;
      }
      set {
        employee = value;
      }
    }

    private string product = "";
    public string Product {
      get {
        return product;
      }
      set {
        product = value;
      }
    }

    private int count = Const.THERE_IS_NOT;
    public int Count {
      get {
        return count;
      }
      set {
        count = value;
      }
    }

    private string provider = "";
    public string Provider {
      get {
        return provider;
      }
      set {
        provider = value;
      }
    }

    public enum WareHouseStatus { Wait, Entered };
    private WareHouseStatus wareStatus = WareHouseStatus.Wait;
    public WareHouseStatus WareStatus {
      get {
        return wareStatus;
      }
      set {
        wareStatus = value;
      }
    }

    public enum RequestStatus { Wait, Passed, Late, Cancelled };
    private RequestStatus status = RequestStatus.Wait;
    public RequestStatus Status {
      get {
        return status;
      }
      set {
        status = value;
      }
    }

    private string comment = "";
    public string Comment {
      get {
        return comment;
      }
      set {
        comment = value;
      }
    }
  }
}