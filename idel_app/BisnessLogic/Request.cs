using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using idel_app.Middle;

namespace idel_app.BisnessLogic {
  public class Request {
    
    #region Properites

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
    private WareHouseStatus wareHouseStatus = WareHouseStatus.Wait;
    public WareHouseStatus WareStatus {
      get {
        return wareHouseStatus;
      }
      set {
        wareHouseStatus = value;
      }
    }

    public enum RequestStatus { Wait, Passed, Late, Cancelled };
    private RequestStatus requestStatus = RequestStatus.Wait;
    public RequestStatus Status {
      get {
        return requestStatus;
      }
      set {
        requestStatus = value;
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

    #endregion

    public Request(int _id, string _title, DateTime _createDate, DateTime _passDate, string _employee, string _product, 
                   string _provider, int _count, WareHouseStatus _wareHouseStatus, RequestStatus _requestStatus, string _comment) {
      Id = _id;
      title = _title;
      createDate = _createDate;
      passDate = _passDate;
      employee = _employee;
      product = _product;
      provider = _provider;
      count = _count;
      wareHouseStatus = _wareHouseStatus;
      requestStatus = _requestStatus;
      comment = _comment;
    }

    public Request() {
      Id = 0;
      title = "Destroy rebels";
      createDate = DateTime.Today;
      passDate = DateTime.Today + new TimeSpan(1, 0, 0, 0);
      employee = "Sidius";
      product = "DeathStar";
      provider = "Darth Vader";
      count = 1;
      wareHouseStatus = WareHouseStatus.Wait;
      requestStatus = RequestStatus.Wait;
      comment = "Work in progress, master!";
    }

    public object[] Properites() {
      return new object[] { id, title, createDate, passDate, employee, product, count, provider, wareHouseStatus, requestStatus, comment };
    }
  }
}