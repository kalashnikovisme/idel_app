using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using idel_app.Middle;

namespace idel_app.BisnessLogic {
  public class Request {
    
    #region Properites

    private BusinessProperity id = new BusinessProperity(Const.THERE_IS_NOT, "Id");
    public int Id {
      get {
        return Int32.Parse(id.Properity.ToString());
      }
      set {
        id.Properity = value;
      }
    }

    private BusinessProperity createDate = new BusinessProperity(new DateTime(), "Дата создания");
    public DateTime CreateDate {
      get {
        return DateTime.Parse(createDate.Properity.ToString());
      }
      set {
        createDate.Properity = value;
      }
    }

    private BusinessProperity passDate = new BusinessProperity(new DateTime(), "Дата сдачи");
    public DateTime PassDate {
      get {
        return DateTime.Parse(passDate.Properity.ToString());
      }
      set {
        passDate.Properity = value;
      }
    }

    private BusinessProperity employee = new BusinessProperity("", "Ответственный");
    public string Employee {
      get {
        return employee.Properity.ToString();
      }
      set {
        employee.Properity = value;
      }
    }

    private BusinessProperity product = new BusinessProperity("", "Продукт");
    public string Product {
      get {
        return product.Properity.ToString();
      }
      set {
        product.Properity = value;
      }
    }

    private BusinessProperity provider = new BusinessProperity("", "Поставщик");
    public string Provider {
      get {
        return provider.Properity.ToString();
      }
      set {
        provider.Properity = value;
      }
    }

    private BusinessProperity count = new BusinessProperity(Const.THERE_IS_NOT, "Количество");
    public int Count {
      get {
        return Int32.Parse(count.Properity.ToString());
      }
      set {
        count.Properity = value;
      }
    }

    private BusinessProperity wareStatus = new BusinessProperity(BLogicConst.REQUEST_WAIT_WARE, "Cклад");
    public bool WareStatus {
      get {
        return Boolean.Parse(wareStatus.Properity.ToString());
      }
      set {
        wareStatus.Properity = value;
      }
    }

    private BusinessProperity requestPassStatus = new BusinessProperity(BLogicConst.REQUEST_WAIT, "Статус");
    public bool RequestPassStatus {
      get {
        return Boolean.Parse(requestPassStatus.Properity.ToString());
      }
      set {
        requestPassStatus.Properity = value;
      }
    }

    private BusinessProperity comment = new BusinessProperity("", "Комментарии");
    public string Comment {
      get {
        return comment.Properity.ToString();
      }
      set {
        comment.Properity = value;
      }
    }

    #endregion

    public Request(int _id, string _title, DateTime _createDate, DateTime _passDate, string _employee, string _product, 
                   string _provider, int _count, bool _wareHouseStatus, bool _requestStatus, string _comment) {
      Id = _id;
      CreateDate = _createDate;
      PassDate = _passDate;
      Employee = _employee;
      Product = _product;
      Provider = _provider;
      Count = _count;
      WareStatus = _wareHouseStatus;
      RequestPassStatus = _requestStatus;
      Comment = _comment;
    }

    public Request() {
      Id = 0;
      CreateDate = DateTime.Today;
      PassDate = DateTime.Today + new TimeSpan(1, 0, 0, 0);
      Employee = "Sidius";
      Product = "Death Star";
      Provider = "Dark Emperror";
      Count = 1;
      WareStatus = BLogicConst.REQUEST_WAIT_WARE;
      RequestPassStatus = BLogicConst.REQUEST_WAIT;
      Comment = "Work in progress, master!";
    }

    public object[] Properites() {
      return new object[] { Id, CreateDate, PassDate, Employee, Product, Count, Provider, WareStatus, RequestPassStatus, Comment };
    }

    public List<string> ProperitesNames() {
      return new List<string>() { id.Name, createDate.Name, passDate.Name, employee.Name, product.Name, count.Name, provider.Name };
    }
  }
}